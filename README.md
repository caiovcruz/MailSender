![Header.png](https://dev.azure.com/devdmcard/feb20a25-21c3-4408-acdd-5e0a43874e87/_apis/git/repositories/5618bb4a-b883-4572-a96f-a562d160026b/items?path=/Src/JobIntegracaoWoli/assets/header-dm.jpg&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=docs/readme&resolveLfs=true&%24format=octetStream&api-version=5.0)


<div style="margin: 1em; line-height: 1.1;">


# MailSender

## 📝 Sobre o Projeto
A aplicação é uma biblioteca para envio de e-mail.

## 🚀 Tecnologias Utilizadas
O projeto MailSender é desenvolvido utilizando as seguintes tecnologias:
- .NET 8.0

## ⚙️ Como configurar e executar
Para executar o projeto localmente, siga as etapas abaixo:

1. Certifique-se de ter o ambiente de desenvolvimento .NET 8.0 configurado em sua máquina.
2. Clone este repositório em sua máquina local.
3. Abra o seu projeto no Visual Studio ou em sua IDE de preferência.
4. Adicione as referências necessárias para o MailSender
5. Configure uma section "SmtpMail" com os dados no arquivo de configuração `appsettings.json`.
	5.1   "SmtpMail": {
			"Host": "smtp-mail.outlook.com",
			"Port": 587,
			"Ssl": true,
			"User": "caio.vcruz@outlook.com",
			"Password": "xxxxxxxxxxx",
			"Sender": "caio.vcruz@outlook.com",
			"Recipients": [ "caio.vcruz@outlook.com" ]
		  }
6.	Faça a injeção de dependência da classe de envio de e-mail e configuração SMTP (importante seguir estrutura exemplo na seção 5.
	6.1	services.AddScoped<IMailSender, MailSender>();
	6.2	services.AddSingleton<ISmtpConfigurations>(config.GetSection("SmtpMail").Get<SmtpConfigurations>());
7.	Feita toda configuração a biblioteca está pronta para ser utilizada. O método de exemplo abaixo mostra uma forma de utilizá-la.

	public async Task SendMailAsync(IList<Attachment> attachments = null)
	{
		var email = new MailConfigurations();

		email.Subject = $"Teste envio de e-mail {GetType().Name}";

		email.Body = $@"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
						Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure 
						dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non 
						proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

		email.IsHtml = false;

		email.FromAddress = new MailAddress(_configuration.GetValue<string>("SmtpMail:Sender"));

		var recipients = _configuration.GetSection("SmtpMail:Recipients").Get<IEnumerable<string>>();

		foreach (var recipient in recipients)
		{
			email.ToAddressList.Add(new MailAddress(recipient));
		}

		await _mailSender.SendAsync(email, attachments);
	}
	
	E onde chamar o método, caso deseje poderá criar anexos(attachments) como abaixo, ou somente chamar a última linha:
	
	var attachments = new List<Attachment>
	{
		new Attachment(nomeArquivoLocal)
	};

	await SendMailAsync(attachments);

</div>