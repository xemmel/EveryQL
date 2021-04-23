Clear-Host;
docker run --rm -p 8080:80 "witcontainerrepo.azurecr.io/everygraph:$($buildVersion)"
