# Experiments with IdentityServer4 [QuickStarts](https://identityserver4.readthedocs.io/en/release/quickstarts/) in ASP.NET Core

Projects
--------

* IdentityServer: authentication server. Allows access to a list of APIs (scopes) using client credentials.
  * Discovery document: http://localhost:5000/.well-known/openid-configuration
  * Client credentals for scope `api1`: `client:secret`

* WebAPI: secured API `api1`.
  * Endoint: http://localhost:5001/api/identity

* Client: makes a HTTP request to the API using a token retrieved from the authentication server using the credentials `client:secret` for the scope `api1`.
