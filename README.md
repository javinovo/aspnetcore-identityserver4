# Experiments with IdentityServer4 [QuickStarts](https://identityserver4.readthedocs.io/en/release/quickstarts/) in ASP.NET Core

Projects
--------

* IdentityServer: authentication server. Allows access to a list of APIs (scopes) using client or user credentials.
  * Discovery document: http://localhost:5000/.well-known/openid-configuration
  * Declares two clients:
    * `client` for client authentication.
	* `ro.client` for user authentication.

* WebAPI: secured API `api1`.
  * Endoint: http://localhost:5001/api/identity

* Client: makes a HTTP request to the API using a token retrieved from the authentication server for the scope `api1`. It requires one of two possible command line arguments:
  * `client`: requests the token using client credentials.
  * `user`: requests the token using user credentials.
