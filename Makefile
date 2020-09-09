# Project Variables
PROJECT_NAME ?= Stock
ORG_NAME ?= Stock
REPO_NAME ?= Stock

.PHONY: mig db 

mig:
	cd ./Stock.Data && dotnet ef --startup-project ../Stock.Api/ migrations add $(name) && cd ..

db:
	cd ./Stock.Data && dotnet ef --startup-project ../Stock.Api/ database update && cd ..
	