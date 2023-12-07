using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Threading;

class prueba_automatizada
{
    static void Main()
    {
        try
        {
            var opciones = new EdgeOptions();
            var driver = new EdgeDriver(opciones);

            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fotos");

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            driver.Navigate().GoToUrl("https://clientes.eps.com.do/Login.aspx");
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto1.png"));

            Thread.Sleep(2000);
            var usuario = driver.FindElement(By.Id("lUser"));
            usuario.SendKeys("pabloeliasbasiliodejesus@gmail.com");
            Thread.Sleep(2000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto2.png"));

            var password = driver.FindElement(By.Id("lPass"));
            password.SendKeys("eliasgamer123");
            Thread.Sleep(2000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto3.png"));

            var loginButton = driver.FindElement(By.LinkText("Iniciar sesión"));
            loginButton.Click(); 
            Thread.Sleep(5000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto4.png"));

            var miCuentaLink = driver.FindElement(By.Id("lMicuenta"));
            miCuentaLink.Click();
            Thread.Sleep(5000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto5.png"));
 
            var misrastreo = driver.FindElement(By.Id("lRastreo"));
            misrastreo.Click();
            Thread.Sleep(5000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto6.png"));

            var prealerta = driver.FindElement(By.Id("lPreAlerta"));
            prealerta.Click();
            Thread.Sleep(5000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto7.png"));

            var prealerta2 = driver.FindElement(By.Id("lPreAlertaExpress"));
            prealerta2.Click();
            Thread.Sleep(5000);
            Capturapantalla(driver, Path.Combine(carpeta, "foto8.png"));

            // Obtener la ruta completa del informe HTML
            Generarhtml(carpeta);
            string reportepath = Path.Combine(carpeta, "Reporte_final.html");
            Console.WriteLine($"Informe HTML creado en: {carpeta}");

            // Abrir el informe HTML con el programa predeterminado (normalmente el navegador web)
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = reportepath,
                UseShellExecute = true
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error: {ex.Message}");
        }
    }

    // Función para capturar la pantalla y guardarla en un archivo
    static void Capturapantalla(IWebDriver driver, string Path)
    {
        try
        {
            ITakesScreenshot controladorCaptura = (ITakesScreenshot)driver;
            Screenshot Captura = controladorCaptura.GetScreenshot();
            Captura.SaveAsFile(Path, ScreenshotImageFormat.Png);
            Console.WriteLine($"Captura de pantalla guardada en: {Path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al capturar la pantalla: {ex.Message}");
        }
    }

    static void Generarhtml(string carpeta)
    {
        try
        {
            string reportepath = Path.Combine(carpeta, "Reporte_final.html");

            using (StreamWriter sw = new StreamWriter(reportepath))
            {
                sw.WriteLine("<html>");
                sw.WriteLine("<head>");
                sw.WriteLine("<title>Reporte Final</title>");
                sw.WriteLine("<style>");
                sw.WriteLine("body { background-color: black; color: white; }");  // Fondo negro y color de texto blanco
                sw.WriteLine(".image-container { margin-bottom: 20px; border: 2px solid white; padding: 10px; }");  // Cuadro alrededor de cada imagen
                sw.WriteLine("</style>");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");

                sw.WriteLine("<br><center><h1>Informe Final de Prueba Automatizada:</h1></center><hr><br>");

                string[] imagenes = Directory.GetFiles(carpeta, "*.png");

                // Array de textos asociados a cada imagen (asegúrate de que haya tantos elementos como imágenes)
                string[] textos = new string[]
                {
                    "Login de Clientes.",
                    "Introducir correo.",
                    "Introducir contraseña.",
                    "Pagina de inicio.",
                    "Ver perfil.",
                    "Ver estados de paquetes",
                    "Mandar alertas a los paquetes proximo al curier de manera detallada",
                    "Alertar al curier pero mas rapido",
                };

                // Verificar si la cantidad de textos coincide con la cantidad de imágenes
                if (imagenes.Length == textos.Length)
                {
                    for (int i = 0; i < imagenes.Length; i++)
                    {
                        sw.WriteLine("<div class='image-container'>");

                        // Agregar la imagen recortada
                        sw.WriteLine($"<center><img src=\"{Path.GetFileName(imagenes[i])}\" alt=\"Captura de pantalla\" style='max-width: calc(100% - 20px); max-height: calc(100vh - 80px);'></center>");

                        // Agregar el texto
                        sw.WriteLine($"<p>{textos[i]}</p>");

                        sw.WriteLine("</div>");
                    }
                }
                else
                {
                    sw.WriteLine("<p style='color: red;'>Error: La cantidad de imágenes y textos no coincide.</p>");
                }

                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
            }
            Console.WriteLine($"Informe HTML creado en: {reportepath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar el informe HTML: {ex.Message}");
        }
    }
}
