

static class APISetup
{
  /** <summary> Esta função configura o Swagger para documentar a aplicação. </summary> */
  public static WebApplication Setup(WebApplicationBuilder builder)
  {
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();
    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "Top Seguros Brasil Policy Engine",
        Description = "Motor de gerenciamento de apolices para Top Seguros Brasil. Feito com amor <3",
      });
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();

    return app;
  }
}