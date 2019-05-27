# Bruno Campiol

Small project for personal web page.

## Project Build

Copy following json example into appsettings.json and appsettings.Development.json files.

```javascript
{
  "AppSettingsFlavor": "DEVELOPMENT",
  "ConnectionString": "Data Source=.;Initial Catalog=BrunoCampiol;Integrated Security=True",
  "IpApiService": {
    "Host": "http://ip-api.com",
    "Resource": "/json/"
  },
  "GitHub": {
    "ClientId": "ClientIdValue",
    "ClientSecret": "ClientSecretValue"
  },
  "Twitter": {
    "ApiKey": "ApiKeyValue",
    "ApiSecret": "ApiSecretValue"
  },
  "Facebook": {
    "AppId": "AppIdValue",
    "AppSecret": "AppSecretValue"
  },
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

