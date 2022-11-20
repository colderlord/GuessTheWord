#Чтение параметров
$values = Get-Content ".\.env" | Out-String | ConvertFrom-StringData

$certName = $values.certName + ".pfx"
$certPass = $values.certPass

dotnet dev-certs https -ep $env:userprofile\.aspnet\https\$certName -p $certPass
dotnet dev-certs https --trust

docker-compose -f docker-compose.yml up -d --build