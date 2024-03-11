# Readme

Source code is all in the src folder at root.

## WebApi
Is the start-up project.
in your appsettings file you will need the following entries

```  "ApplicationInsightsConn": string,
  "DBConnection": string,
  "WeavrSettings": {
    "BaseAddress": string,
    "ProfileId": string,
    "ApiKey":string
  }
```

## NextJS App.

_.env.local_ has one entry, the base address to the Web Api project (including the /api segement)
NEXT_PUBLIC_REMOTE_API_ADDR=String

Start the app with `npm run dev`.
