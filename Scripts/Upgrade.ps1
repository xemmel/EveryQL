Clear-Host;
git pull;

$buildVersion = cat .\EveryGraph\GraphQL\Base\EveryGraphQuery.cs | Select-String -Pattern "(?<=_ => \`$`").*(?=`")" | 
Select-Object -ExpandProperty Matches | 
Select-Object -ExpandProperty Value;

docker build  -t "witcontainerrepo.azurecr.io/everygraph:$($buildVersion)" .

az acr login -n witcontainerrepo

## Push image to azure registry
docker push "witcontainerrepo.azurecr.io/everygraph:$($buildVersion)"

## Update WebApp with image from Repo
az webapp config container set -g graphazure -n everygraph `
   --docker-custom-image-name "witcontainerrepo.azurecr.io/everygraph:$($buildVersion)"

