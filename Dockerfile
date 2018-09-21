FROM centos
RUN rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm
RUN yum -y install libicu
RUN yum -y install dotnet-sdk-2.1

RUN dotnet build socket.csproj -c Release -o /dotnetdemo
RUN dotnet publish socket.csproj -c Release -o /dotnetdemo

###ENIRYPOINT ["dotnet","/dotnetdemo/socket.dll"]
EXPOSE 5678
CMD ["dotnet","/dotnetdemo/socket.dll"]
