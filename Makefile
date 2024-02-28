FrontEndPage := ./web

install:
	cd $(FrontEndPage) && pnpm install

build-frontend:
	cd $(FrontEndPage) && pnpm run build

.PHONY: frontend
frontend: install build-frontend

.PHONY: backend
backend:
	dotnet restore
	dotnet build

.PHONY: build-all
build-all: frontend backend