using BrunoCampiol.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrunoCampiol.Service.Interface
{
    public interface IIPGeolocationService
    {
        VISITORS GetVisitorInformation(string ipAddress);
    }
}