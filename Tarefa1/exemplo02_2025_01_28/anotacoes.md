instalar nvm

$ nvm install node@latest

export USUARIO=flcs
export PROJETO=exemplo02_2025_01_28

git init .
# git remote add origin https://gitlab.com/$(USUARIO)/$(PROJETO).git
git remote add origin https://gitlab.com/flcs/exemplo02_2025_01_28.git

echo 'node_modules/' >> .gitignore
echo 'dist/' >> .gitignore

npm init -y

git add .
git commit -m 'inicio'
git push -u origin main

npm install express
npm install -D @types/express

npm install -D typescript
npm install -D @types/node
npm install -D ts-node-dev

npx tsc --init

git pull ; git add . ; git commit -m 'alteracoes feitas' ; git push

instalar extensao "Rest Client" no VSCode

== Testes ==============================

$ npm install -D jest @types/jest ts-jest

$ npx jest --init

The following questions will help Jest to create a suitable configuration for your project

✔ Would you like to use Jest when running "test" script in "package.json"? … yes
✔ Would you like to use Typescript for the configuration file? … no
✔ Choose the test environment that will be used for testing › node
✔ Do you want Jest to add coverage reports? … yes
✔ Which provider should be used to instrument code for coverage? › babel
✔ Automatically clear mock calls, instances, contexts and results before every test? … yes


