# demo-webapp-api-auth


### Add JWT validation in API Management

Using the information from the client token add JWT validation rule to API Managment

```xml
<validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized. Access token is missing or invalid.">
    <openid-config url="https://login.microsoftonline.com/{tenant}/.well-known/openid-configuration" />
    <required-claims>
        <claim name="aud" match="any">
            <value>api://{apiAppID}</value>
            <value>{apiAppID}</value>
        </claim>
        <claim name="appid" match="any">
            <value>{clientAppID}</value>
        </claim>
    </required-claims>
</validate-jwt>
```

## References