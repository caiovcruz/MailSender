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
```json
"SmtpMail": {
  "Host": "smtp-mail.outlook.com",
  "Port": 587,
  "Ssl": true,
  "User": "caio.vcruz@outlook.com",
  "Password": "xxxxxxxxxxx",
  "Sender": "caio.vcruz@outlook.com",
  "Recipients": [ "caio.vcruz@outlook.com" ]
}
```
7.	Faça a injeção de dependência da classe de envio de e-mail e configuração SMTP (importante seguir estrutura exemplo na seção 5).
```dotnet
services.AddScoped<IMailSender, MailSender>();
services.AddSingleton<ISmtpConfigurations>(config.GetSection("SmtpMail").Get<SmtpConfigurations>());
```
8.	Feita toda configuração a biblioteca está pronta para ser utilizada. O método de exemplo abaixo mostra uma forma de utilizá-la.
```dotnet
public async Task SendMailAsync(string subject, string body, IList<Attachment> attachments = null)
{
	var email = new MailConfigurations();

	email.Subject = subject;

	email.Body = body;

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
```

</div>
