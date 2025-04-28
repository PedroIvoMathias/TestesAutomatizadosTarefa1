# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Dependendo do sistema operacional dos computadores host que compilarão ou executarão os contêineres, a imagem especificada na instrução FROM pode precisar ser alterada.
# Para obter mais informações, consulte https://aka.ms/containercompat.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EnviarTrabalhos/EnviarTrabalhos.csproj", "EnviarTrabalhos/"]
RUN dotnet restore "./EnviarTrabalhos/EnviarTrabalhos.csproj"
COPY . .
WORKDIR "/src/EnviarTrabalhos"
RUN dotnet build "./EnviarTrabalhos.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EnviarTrabalhos.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnviarTrabalhos.dll"]

RUN useradd -m myappuser
USER myappuser 

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet EnviarTrabalhos.dll

# Variável de ambiente para produção
#ENV ASPNETCORE_URLS=http://+:5000

# Porta exposta
#EXPOSE 5000

# Comando para rodar
#ENTRYPOINT ["dotnet", "EnviarTrabalhos.dll"]
