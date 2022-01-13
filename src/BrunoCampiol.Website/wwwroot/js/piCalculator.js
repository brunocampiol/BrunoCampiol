// https://www.htmlgoodies.com/html5/tutorials/introducing-html-5-web-workers-bringing-multi-threading-to-javascript.html
//Bailey–Borwein–Plouffe formula


// The "use strict" directive was new in ECMAScript version 5.
// It is not a statement, but a literal expression, ignored by earlier versions of JavaScript.
// The purpose of "use strict" is to indicate that the code should be executed in "strict mode".
// With strict mode, you can not, for example, use undeclared variables.
'use strict';

function chudnovsky(digits) {

    /*
     * Compute pi to the specified number of decimal digits using the
     * Chudnovsky Algorithm.
     *                               _____
     *                     426880 * /10005
     *  pi = ---------------------------------------------
     *         _inf_
     *         \     (6*k)! * (13591409 + 545140134 * k)
     *          \    -----------------------------------
     *          /     (3*k)! * (k!)^3 * (-640320)^(3*k)
     *         /____
     *          k=0
     *
     * http://en.wikipedia.org/wiki/Pi#Rapidly_convergent_series
     *
     * Adapted from the C code of Brian Hall at
     * http://beej.us/blog/data/pi-chudnovsky-gmp/chudnovsky_c.txt
     *
     * parameter: digits {number} the number of decimal digits to compute
     * returns: Pi {Decimal}
     */

    importScripts('../lib/decimal/decimal.min.js');

    Decimal.precision = digits + 2;

    function factorial(n) {
        let i = 2;
        let r = new Decimal(1);
        for (i; i <= n; r = r.times(i++));
        return r;
    }

    // The number of decimal digits the algorithm generates per iteration.
    let digits_per_iteration = 14.1816474627254776555;
    let iterations = (digits / digits_per_iteration) + 1;

    let a = new Decimal(13591409);
    let b = new Decimal(545140134);
    let c = new Decimal(-640320);

    let numerator, denominator;
    let sum = new Decimal(0);

    let roundIterations = Math.ceil(iterations);
    let previewDigits = 1;
    let previewIteration = Math.ceil(digits / roundIterations);

    for (let k = 0; k < iterations; k++) {

        // (6k)! * (13591409 + 545140134k)
        numerator = factorial(6 * k).times(a.plus(b.times(k)));

        // (3k)! * (k!)^3 * -640320^(3k)
        denominator = factorial(3 * k).times(factorial(k).pow(3)).times(c.pow(3 * k));

        sum = sum.plus(numerator.div(denominator));

        // TODO: change the preview so that it calculates faster
        // Preview for UI - user info
        let preview = Decimal.sqrt(10005).times(426880).div(sum).toSD(previewDigits).toString();
        let previewMessage = 'Calculating PI for ' + digits + ' digits... ' + k + ' of ' + roundIterations + ' iterations. <br/><br/> ' + preview;
        previewDigits += previewIteration;
        self.postMessage({ 'PiValue': previewMessage });
    }

    // pi = ( sqrt(10005) * 426880 ) / sum
    //return Decimal.sqrt(10005).times(426880).div(sum).toSD(digits);
    // Final value - should be very similar to real value
    self.postMessage({ 'PiValue': Decimal.sqrt(10005).times(426880).div(sum).toSD(digits).toString() });
}

//wait for the start 'CalculatePi' message
//e is the event and e.data contains the JSON object
self.onmessage = function (e) {
    chudnovsky(e.data.value);
};