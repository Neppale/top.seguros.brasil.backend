

static class APISetup
{
  /** <summary> Esta função configura o Swagger para documentar a aplicação. </summary> */
  public static WebApplication Setup(WebApplicationBuilder builder)
  {
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();
    var securityScheme = new OpenApiSecurityScheme()
    {
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      Scheme = "Bearer",
      BearerFormat = "JWT",
      In = ParameterLocation.Header,
      Description = "JSON Web Token based security",
    };

    var securityReq = new OpenApiSecurityRequirement() { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } } };

    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "Top Seguros Brasil Policy Engine",
        Description = "Motor de gerenciamento de apolices para Top Seguros Brasil. Feito com amor <3",
      });
      options.AddSecurityDefinition("Bearer", securityScheme);
      options.AddSecurityRequirement(securityReq);

    });

    builder.Services.AddAuthentication(o =>
      {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.
      AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.
      AuthenticationScheme;
        o.DefaultScheme = JwtBearerDefaults.
      AuthenticationScheme;
      }).AddJwtBearer(o =>
        {
          o.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
          };
        });

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    builder.Services.AddAuthorization();

    string temporaryDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Temp");

    return app;
  }
}