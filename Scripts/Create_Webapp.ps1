Clear-Host;
## REQUIRES GRAPHAZURE RG and PLAN!

## WebApp from image Azure Container Repo
az webapp create -g graphazure -n everygraph `
     --plan graphazureplan --deployment-container-image-name "witcontainerrepo.azurecr.io/everygraph:$($buildVersion)" `
	  --docker-registry-server-user witcontainerrepo `
	  --docker-registry-server-password "G4=3b8OA6SoZk2B0zj/Eo2MvjZ+s/mS2"


https://everygraph.azurewebsites.net/ui/playground