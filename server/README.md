dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\guesstheword.pfx -p !Q2w3e4r5t
dotnet dev-certs https --trust

docker build --no-cache -t guesstheword:build .

docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certifi
cates__Default__Password="!Q2w3e4r5t" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/guesstheword.pfx -v %USERPROFILE%\.aspnet\https:/https/ --name guesstheworld guesstheword:build