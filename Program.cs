DotEnv.Load();
var environmentVariables = DotEnv.Read();
if (environmentVariables.Count() == 0) throw new Exception("Não foi possível carregar as variáveis de ambiente. Verifique se o arquivo .env está presente na raiz do projeto.");

var builder = WebApplication.CreateBuilder(args);
var app = APISetup.Setup(builder, environmentVariables);

string dbConnectionString = environmentVariables["CONNECTION_STRING"];
SqlConnection connectionString = new SqlConnection(dbConnectionString);
connectionString.Open();

// HEALTHCHECK
app.MapHealthChecks("/healthcheck/");

// CLIENTES 
ClientController.ActivateEndpoints(app: app, connectionString: connectionString, environmentVariables: environmentVariables);

// APOLICES 
PolicyController.ActivateEndpoints(app: app, connectionString: connectionString);

// COBERTURAS
CoverageController.ActivateEndpoints(app: app, connectionString: connectionString);

// OCORRENCIAS
IncidentController.ActivateEndpoints(app: app, connectionString: connectionString);

// TERCEIRIZADOS
OutsourcedController.ActivateEndpoints(app: app, connectionString: connectionString);

// USUARIOS
UserController.ActivateEndpoints(app: app, connectionString: connectionString, environmentVariables: environmentVariables);

// VEICULOS
VehicleController.ActivateEndpoints(app: app, connectionString: connectionString);

// FIPE API
FipeController.ActivateEndpoints(app: app);

app.Run();

