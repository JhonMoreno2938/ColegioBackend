using Colegio.Dato;
using Colegio.Interfaz;
using Colegio.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Configura CORS para permitir solicitudes desde frontend
builder.Services.AddCors(o => o.AddPolicy("AllowFrontend", p =>
    p.AllowAnyOrigin()
     .AllowAnyHeader()
     .AllowAnyMethod()
));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Jwt:Key"])),
        RoleClaimType = "rolUsuario"


    };
});

// Configuracion de las politicas de autorizacion
builder.Services.AddAuthorization(options =>
{
    // Permite SECRETARIO o DOCENTE
    options.AddPolicy("SecretarioODocente", policy =>
        policy.RequireRole("SECRETARIO", "DOCENTE"));

    // Permite SECRETARIO o ESTUDIANTE
    options.AddPolicy("SecretarioOEstudiante", policy =>
        policy.RequireRole("SECRETARIO", "ESTUDIANTE"));

    // Permite solo DOCENTE
    options.AddPolicy("SoloDocente", policy =>
        policy.RequireRole("DOCENTE"));

    // Permite solo ESTUDIANTE
    options.AddPolicy("SoloEstudiante", policy =>
        policy.RequireRole("ESTUDIANTE"));

    //Permite solo SECRETARIO
    options.AddPolicy("SoloSecretario", policy =>
        policy.RequireRole("SECRETARIO"));
});


// Configuración de SwaggerGen para múltiples documentos
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("Departamento", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Departamento",
        Version = "v2",
        Description = "Documentacion para la API de departamento"
    });

    options.SwaggerDoc("Genero", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Genero",
        Version = "v2",
        Description = "Documentacion para la API de genero"
    });

    options.SwaggerDoc("Rh", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Rh",
        Version = "v2",
        Description = "Documentacion para la API de rh"
    });

    options.SwaggerDoc("TipoDocumento", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API TipoDocumento",
        Version = "v2",
        Description = "Documentacion para la API de tipo documento"
    });

    options.SwaggerDoc("TipoFuncionario", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API TipoFuncionario",
        Version = "v2",
        Description = "Documentacion para la API de tipo funcionario"
    });

    options.SwaggerDoc("RolUsuario", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API RolUsuario",
        Version = "v2",
        Description = "Documentacion para la API de rol usuario"
    });

    options.SwaggerDoc("Funcionario", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Funcionario",
        Version = "v2",
        Description = "Documentacion para la API de funcionario"
    });

    options.SwaggerDoc("Usuario", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Usuario",
        Version = "v2",
        Description = "Documentacion para la API de usuario"
    });

    options.SwaggerDoc("TipoSede", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API TipoSede",
        Version = "v2",
        Description = "Documentacion para la API de tipo sede"
    });

    options.SwaggerDoc("Sede", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Sede",
        Version = "v2",
        Description = "Documentacion para la API de sede"
    });

    options.SwaggerDoc("Grado", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Grado",
        Version = "v2",
        Description = "Documentacion para la API de grado"
    });
    
    options.SwaggerDoc("Grupo", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Grupo",
        Version = "v2",
        Description = "Documentacion para la API de grupo"
    });

    options.SwaggerDoc("Jornada", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Jornada",
        Version = "v2",
        Description = "Documentacion para la API de jornada"
    });

    options.SwaggerDoc("NivelEscolaridad", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API NivelEscolaridad",
        Version = "v2",
        Description = "Documentacion para la API de nivel escolaridad"
    });

    options.SwaggerDoc("GradoGrupo", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API GradoGrupo",
        Version = "v2",
        Description = "Documentacion para la API de grado grupo"
    });

    options.SwaggerDoc("JornadaSede", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API JornadaSede",
        Version = "v2",
        Description = "Documentacion para la API de jornada sede"
    });

    options.SwaggerDoc("SedeJornadaGradoGrupo", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API SedeJornadaGradoGrupo",
        Version = "v2",
        Description = "Documentacion para la API de sede jornada grado grupo"
    });

    options.SwaggerDoc("AsignaturaNivelEscolaridad", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API AsignaturaNivelEscolaridad",
        Version = "v2",
        Description = "Documentacion para la API de asignatura nivel escolaridad"
    });

    options.SwaggerDoc("Asignatura", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Asignatura",
        Version = "v2",
        Description = "Documentacion para la API asignatura"
    });

    options.SwaggerDoc("AsignaturaGradoGrupo", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API AsignaturaGradoGrupo",
        Version = "v2",
        Description = "Documentacion para la API asignatura grado grupo"
    });

    options.SwaggerDoc("Porcentaje", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Porcentaje",
        Version = "v2",
        Description = "Documentacion para la API porcentaje"
    });

    options.SwaggerDoc("NombrePeriodoAcademico", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API NombrePeriodoAcademico",
        Version = "v2",
        Description = "Documentacion para la API nombre periodo academico"
    });

    options.SwaggerDoc("PeriodoAcademico", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API PeriodoAcademico",
        Version = "v2",
        Description = "Documentacion para la API periodo academico"
    });

    options.SwaggerDoc("Estudiante", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Estudiante",
        Version = "v2",
        Description = "Documentacion para la API estudiante"
    });

    options.SwaggerDoc("Auditoria", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Auditoria",
        Version = "v2",
        Description = "Documentacion para la API auditoria"
    });

    options.SwaggerDoc("FuncionarioAsignatura", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API FuncionarioAsignatura",
        Version = "v2",
        Description = "Documentacion para la API funcionario asignatura"
    });

    options.SwaggerDoc("TipoCalificacionAcademica", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API TipoCalificacionAcademica",
        Version = "v2",
        Description = "Documentacion para la API tipo calificacion academica"
    });

    options.SwaggerDoc("FuncionarioPeriodoAcademico", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API FuncionarioPeriodoAcademico",
        Version = "v2",
        Description = "Documentacion para la API funcionario periodo academico"
    });

    options.SwaggerDoc("EstudiantePeriodoAcademico", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API EstudiantePeriodoAcademico",
        Version = "v2",
        Description = "Documentacion para la API estudiante periodo academico"
    });

    options.SwaggerDoc("Competencia", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Competencia",
        Version = "v2",
        Description = "Documentacion para la API cometencia"
    });

    options.SwaggerDoc("CompetenciaEstudiante", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API CompetenciaEstudiante",
        Version = "v2",
        Description = "Documentacion para la API competencia estudiante"
    });

    // Predicado para asociar controladores a documentos Swagger específicos
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
        return apiDesc.GroupName == docName;
    });

    // Configuración de Seguridad (Bearer Token) en Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Por favor ingrese el token JWT con el prefijo 'Bearer' como 'Bearer [token]'",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Registro de las dependencias (Inyección de Dependencias)
builder.Services.AddScoped<IDepartamento, DepartamentoDato>();
builder.Services.AddScoped<DepartamentoServicio>();
builder.Services.AddScoped<IGenero, GeneroDato>();
builder.Services.AddScoped<GeneroServicio>();
builder.Services.AddScoped<IRh, RhDato>();
builder.Services.AddScoped<RhServicio>();
builder.Services.AddScoped<ITipoDocumento, TipoDocumentoDato>();
builder.Services.AddScoped<TipoDocumentoServicio>();
builder.Services.AddScoped<ITipoFuncionario, TipoFuncionarioDato>();
builder.Services.AddScoped<TipoFuncionarioServicio>();
builder.Services.AddScoped<IRolUsuario, RolUsuarioDato>();
builder.Services.AddScoped<RolUsuarioServicio>();
builder.Services.AddScoped<IFuncionario, FuncionarioDato>();
builder.Services.AddScoped<FuncionarioServicio>();
builder.Services.AddScoped<IUsuario, UsuarioDato>();
builder.Services.AddScoped<UsuarioServicio>();
builder.Services.AddScoped<ITipoSede, TipoSedeDato>();
builder.Services.AddScoped<TipoSedeServicio>();
builder.Services.AddScoped<ISede, SedeDato>();
builder.Services.AddScoped<SedeServicio>();
builder.Services.AddScoped<IGrado, GradoDato>();
builder.Services.AddScoped<GradoServicio>();
builder.Services.AddScoped<IGrupo, GrupoDato>();
builder.Services.AddScoped<GrupoServicio>();
builder.Services.AddScoped<IJornada, JornadaDato>();
builder.Services.AddScoped<JornadaServicio>();
builder.Services.AddScoped<INivelEscolaridad, NivelEscolaridadDato>();
builder.Services.AddScoped<NivelEscolaridadServicio>();
builder.Services.AddScoped<IGradoGrupo, GradoGrupoDato>();
builder.Services.AddScoped<GradoGrupoServicio>();
builder.Services.AddScoped<IJornadaSede, JornadaSedeDato>();
builder.Services.AddScoped<JornadaSedeServicio>();
builder.Services.AddScoped<ISedeJornadaGradoGrupo, SedeJornadaGradoGrupoDato>();
builder.Services.AddScoped<SedeJornadaGradoGrupoServicio>();
builder.Services.AddScoped<IAsignaturaNivelEscolaridad, AsignaturaNivelEscolaridadDato>();
builder.Services.AddScoped<AsignaturaNivelEscolaridadServicio>();
builder.Services.AddScoped<IAsignatura, AsignaturaDato>();
builder.Services.AddScoped<AsignaturaServicio>();
builder.Services.AddScoped<IAsignaturaGradoGrupo, AsignaturaGradoGrupoDato>();
builder.Services.AddScoped<AsignaturaGradoGrupoServicio>();
builder.Services.AddScoped<IPorcentaje, PorcentajeDato>();
builder.Services.AddScoped<PorcentajeServicio>();
builder.Services.AddScoped<INombrePeriodoAcademico, NombrePeriodoAcademicoDato>();
builder.Services.AddScoped<NombrePeriodoAcademicoServicio>();
builder.Services.AddScoped<IPeriodoAcademico, PeriodoAcademicoDato>();
builder.Services.AddScoped<PeriodoAcademicoServicio>();
builder.Services.AddScoped<IEstudiante, EstudianteDato>();
builder.Services.AddScoped<EstudianteServicio>();
builder.Services.AddScoped<IAuditoria, AuditoriaDato>();
builder.Services.AddScoped<AuditoriaServicio>();
builder.Services.AddScoped<IFuncionarioAsignatura, FuncionarioAsignaturaDato>();
builder.Services.AddScoped<FuncionarioAsignaturaServicio>();
builder.Services.AddScoped<ITipoCalificacionAcademica, TipoCalificacionAcademicaDato>();
builder.Services.AddScoped<TipoCalificacionAcademicaServicio>();
builder.Services.AddScoped<IFuncionarioPeriodoAcademico, FuncionarioPeriodoAcademicoDato>();
builder.Services.AddScoped<FuncionarioPeriodoAcademicoServicio>();
builder.Services.AddScoped<IEstudiantePeriodoAcademico, EstudiantePeriodoAcademicoDato>();
builder.Services.AddScoped<EstudiantePeriodoAcademicoServicio>();
builder.Services.AddScoped<ICompetencia, CompetenciaDato>();
builder.Services.AddScoped<CompetenciaServicio>();
builder.Services.AddScoped<ICompetenciaEstudiante, CompetenciaEstudianteDato>();
builder.Services.AddScoped<CompetenciaEstudianteServicio>();

var app = builder.Build();

app.UseRouting();


// Configuración del pipeline HTTP.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/Asignatura/swagger.json", "API Asignatura");
    options.SwaggerEndpoint("/swagger/AsignaturaGradoGrupo/swagger.json", "API Asignatura grado grupo");
    options.SwaggerEndpoint("/swagger/AsignaturaNivelEscolaridad/swagger.json", "API Asignatura nivel escolaridad");
    options.SwaggerEndpoint("/swagger/Auditoria/swagger.json", "API Auditoria");
    options.SwaggerEndpoint("/swagger/Competencia/swagger.json", "API Competencia");
    options.SwaggerEndpoint("/swagger/CompetenciaEstudiante/swagger.json", "API Competencia estudiante");
    options.SwaggerEndpoint("/swagger/Departamento/swagger.json", "API Departamento");
    options.SwaggerEndpoint("/swagger/Estudiante/swagger.json", "API Estudiante");
    options.SwaggerEndpoint("/swagger/EstudiantePeriodoAcademico/swagger.json", "API Estudiante periodo academico");
    options.SwaggerEndpoint("/swagger/Funcionario/swagger.json", "API Funcionario");
    options.SwaggerEndpoint("/swagger/FuncionarioAsignatura/swagger.json", "API Funcionario asignatura");
    options.SwaggerEndpoint("/swagger/FuncionarioPeriodoAcademico/swagger.json", "API Funcionario periodo academico");
    options.SwaggerEndpoint("/swagger/Genero/swagger.json", "API Genero");
    options.SwaggerEndpoint("/swagger/Grado/swagger.json", "API Grado");
    options.SwaggerEndpoint("/swagger/GradoGrupo/swagger.json", "API Grado grupo");
    options.SwaggerEndpoint("/swagger/Grupo/swagger.json", "API Grupo");
    options.SwaggerEndpoint("/swagger/Jornada/swagger.json", "API Jornada");
    options.SwaggerEndpoint("/swagger/JornadaSede/swagger.json", "API Jornada sede");
    options.SwaggerEndpoint("/swagger/NivelEscolaridad/swagger.json", "API Nivel escolaridad");
    options.SwaggerEndpoint("/swagger/NombrePeriodoAcademico/swagger.json", "API Nombre periodo academico");
    options.SwaggerEndpoint("/swagger/PeriodoAcademico/swagger.json", "API Periodo academico");
    options.SwaggerEndpoint("/swagger/Porcentaje/swagger.json", "API Porcentaje");
    options.SwaggerEndpoint("/swagger/Rh/swagger.json", "API Rh");
    options.SwaggerEndpoint("/swagger/RolUsuario/swagger.json", "API Rol usuario");
    options.SwaggerEndpoint("/swagger/Sede/swagger.json", "API Sede");
    options.SwaggerEndpoint("/swagger/SedeJornadaGradoGrupo/swagger.json", "API Sede jornada grado gurpo");
    options.SwaggerEndpoint("/swagger/TipoCalificacionAcademica/swagger.json", "API Tipo calificacion academica");
    options.SwaggerEndpoint("/swagger/TipoDocumento/swagger.json", "API Tipo documento");
    options.SwaggerEndpoint("/swagger/TipoFuncionario/swagger.json", "API Tipo funcionario");
    options.SwaggerEndpoint("/swagger/TipoSede/swagger.json", "API Tipo sede");
    options.SwaggerEndpoint("/swagger/Usuario/swagger.json", "API Usuario");
    //options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
