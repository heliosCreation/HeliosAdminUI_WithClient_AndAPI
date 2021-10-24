# HeliosAdminUI_Client_API
The main goal of this repository is to demonstrate how a client application can implement a flow using a centralized Identity provider.<br/>
Will be at use here: 
<ol>
  <li>
    <a href="https://identityserver4.readthedocs.io/en/latest/">Identity Server 4</a>, the amazing project of Dominick Baier and Brock Allen.<br/> 
    Working along with it, will be found my <a href="https://github.com/heliosCreation/IdentityServer-HeliosAdminUI">HeliosAdminUI</a> project to offer a UI, a User store and data persistency.
  </li>
  <li>
    A REST API built with .Net 5 and following the Clean Architecture principles.<br/>
    Point of interest: <br/>
      <ul>
        <li>
          CQRS pattern with use of the IPipeline Behaviour but here, <strong> No Exception thrown! </strong>. :) 
        </li>
        <li>
          Generic Repository Pattern.
        </li>
        <li>
          Endpoints protected through JWT Bearer authentication.
        </li>
        <li>
          Swagger UI as a support for the documentation and the development support.
        </li>
      </ul> 
  </li>
  <li>
    A MVC client built with .Net Core 3.1<br/>
    Points of interested: <br/>
        <ul>
          <li>
            Demonstration of the configuration of Identity server required parameters on a client application. 
          </li>
          <li>
            Use of the HttpClientFactory to build default HttpClient. 
          </li>
          <li>
              Centralized <i>BearerTokenHandler</i> to set the access token in one place for the API request and make use of the refresh token.
          </li>
          <li>
            A User Identity built from two pieces. First one from the IDP for generic information. Second piece from the API for the User's information only relative to the API itself.
          </li>
          <li>
              Tired of processing each API Response for your request? Take a look at the generic API Response Parser of the project. 
          </li>
        </ul>
  </li>
  An API Gateway built with Ocelot, to route the Client request to the appropriate Api Endpoint.
  <li>
  
  </li>
</ol>
 
 
 ## Installation
 <i>General requirement</i> - <br/>
  In order for those projects to work, you'll need to have: <br/>
  <ul>
    <li>
      An instance of SQL server installed. If you need help with that, here's a <a href="https://computingforgeeks.com/install-sql-server-developer-edition-on-windows-server/">cool guide</a>. 
    </li>
    <li>
    The .Net Core 3.1 (<a href="https://dotnet.microsoft.com/download/dotnet/3.1"> Click here!</a>) and the .Net 5 (<a href="https://dotnet.microsoft.com/download/dotnet/5.0">Click here!</a>framework installed. 
    </li>
  </ul>,
