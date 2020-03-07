ARG BUILD_ARG=Release
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ARG BUILD_ARG
ENV BUILD_MODE=$BUILD_ARG
# RUN echo "----buildmode: ${BUILD_MODE}-------"
ENV DEBIAN_FRONTEND noninteractive
RUN if [ "${BUILD_ARG}" = "Debug" ]; then \
        apt-get update && \
        apt-get install -y --no-install-recommends apt-utils && \
        apt-get install curl unzip procps -y && \
        curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /app/debugtool/vsdbg; \       
    else \
        echo "*Release*"; \
    fi
ENV DEBIAN_FRONTEND teletype

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
ARG BUILD_ARG
ENV BUILD_MODE=$BUILD_ARG
WORKDIR /src

COPY ["./Applications/identityServer/identityServer.csproj", "./Applications/identityServer/"]
# COPY ["./Applications/Libraries/IdentityService.Core/IdentityService.Core.csproj", "./Applications/Libraries/IdentityService.Core/"]
# COPY ["./Applications/Libraries/IdentityService.Data/IdentityService.Data.csproj", "./Applications/Libraries/IdentityService.Data/"]
# COPY ["./Applications/Libraries/IdentityService.Service/IdentityService.Service.csproj", "./Applications/Libraries/IdentityService.Service/"]

RUN dotnet restore "./Applications/identityServer/identityServer.csproj"

COPY ./Applications/identityServer ./Applications/identityServer
# COPY ./Applications/Libraries ./Applications/Libraries

RUN dotnet build "./Applications/identityServer/identityServer.csproj" -c ${BUILD_MODE} -o /app/build

# RUN echo "$(ls -la /app/build)"

FROM build AS publish
ARG BUILD_ARG
ENV BUILD_MODE=$BUILD_ARG
RUN dotnet publish "./Applications/identityServer/identityServer.csproj" -c ${BUILD_MODE} -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "identityServer.dll"]

