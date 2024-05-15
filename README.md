# Email Sender 📧

Welcome to **Email Sender**, a web application built using ASP.NET MVC that allows users to compose and send emails with customizable recipients, subject, and body.

## ✨ Features

- **Compose Email**: Fill out recipient details (To, CC, BCC), subject, and body to send an email.
- **Send Email**: Utilizes SMTP to send emails securely.
- **Clear Form**: Option to reset all input fields.

## 💻 Technologies Used

- **ASP.NET MVC**: Framework for web development.
- **C#**: Language used for server-side programming.
- **HTML/CSS**: Frontend design and styling.
- **Bootstrap**: Frontend framework for responsive UI.
- **MailMessage Class**: Used for composing emails.
- **SmtpClient Class**: Handles SMTP settings for sending emails securely.

## 🛠️ Getting Started

To run this project locally, follow these steps:

1. **Clone the Repository**

   ```bash
   https://github.com/Mustafabharmal/EmailSender.git
   ```

2. **Open Project in Visual Studio**

   - Launch Visual Studio and open the project.
   - Ensure all required dependencies for ASP.NET MVC are installed.

3. **Configure SMTP Settings**

   - Update the SMTP settings in `EmailController.cs` to match your email provider (e.g., Gmail).

     ```csharp
     using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
     {
         smtpClient.Port = 587;
         smtpClient.Credentials = new NetworkCredential("your_email@gmail.com", "your_password");
         smtpClient.EnableSsl = true;
         // Send the email
         smtpClient.Send(message);
     }
     ```

4. **Build and Run**

   - Build the solution and start the application.
   - Access the application in your web browser (`http://localhost:{port}/Email/Compose`).

5. **Compose and Send Email**

   - Fill out the email details:
     - **To Email**: Recipient's email address.
     - **CC Email**: Optional, carbon copy recipients.
     - **BCC Email**: Optional, blind carbon copy recipients.
     - **Subject**: Email subject line.
     - **Body**: Email content.
   - Click **Send Email** to send the composed email.

6. **Clear Form**

   - Use the **Clear** button to reset all input fields and start over.

## 📂 Project Structure

### Controllers

- **EmailController.cs**: Manages email composition and sending logic.
  - `Compose()`: Displays the email composition form.
  - `Compose(EmailModel model)`: Handles form submission to send the email.

### Models

- **EmailModel.cs**: Defines the model for email composition (ToEmail, CCEmail, BCCEmail, Subject, Body).

### Views

- **Compose.cshtml**: View for the email composition form.

## 🤝 Contributing

Contributions to enhance this project are welcomed! Feel free to fork the repository and submit pull requests.

## 📝 License

This project is licensed under the [MIT License](LICENSE).
