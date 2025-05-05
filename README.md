# TestesAutomatizadosTarefa1
 Tarefa 1 do trabalho referente a matéira de testes automatizados.

Para rodar no vsCode isntalar
code --install-extension ms-dotnettools.csdevkit
code --install-extension ms-dotnettools.csharp
code --install-extension ms-dotnettools.dotnet-interactive-vscode
code --install-extension hbenl.vscode-test-explorer
code --install-extension formulahendry.dotnet-test-explorer
.netSDK



Rodar no Linux:
Instalar o .NET 8
sudo apt update
sudo apt install -y dotnet-sdk-8.0

Instalar o Microsoft Edge no Linux
wget https://packages.microsoft.com/repos/edge/pool/main/m/microsoft-edge-stable/microsoft-edge-stable_*.deb
sudo dpkg -i microsoft-edge-stable_*.deb
sudo apt --fix-broken install
Ou use o Flatpak/Snap caso prefira.

Instalar o EdgeDriver
O EdgeDriver precisa ser compatível com a versão do Edge instalada:
# Descobrir a versão do Edge instalada
microsoft-edge --version
# Acesse https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/
# Baixe o EdgeDriver correspondente e extraia para uma pasta, ex:
sudo mv msedgedriver /usr/local/bin/
sudo chmod +x /usr/local/bin/msedgedriver

Verificar se o EdgeDriver está acessível
msedgedriver --version

Rodar os testes
dotnet test

