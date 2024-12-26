using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;



namespace DotnetSelenium
{
	public class Tests
	{
		

		//public object ExpectedConditions { get; private set; }

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			IWebDriver driver= new EdgeDriver();

			driver.Navigate().GoToUrl("https://www.google.com/"); //navigate to url

			driver.Manage().Window.Maximize(); //maximizing the browser window

			IWebElement webelement = driver.FindElement(By.Name("q")); //find the element

			webelement.SendKeys("Selenium"); //typing the element

			webelement.SendKeys(Keys.Return); //hit enter

		}

		[Test]
		public void BulkyTest()
		{
			// Initialize the WebDriver
			IWebDriver driver = new EdgeDriver();

			try
			{
				// Navigate to the URL
				driver.Navigate().GoToUrl("https://localhost:7048/");
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Set an implicit wait

				// Use WebDriverWait with lambda for dynamic waits
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

				// Wait for the login button and click it
				IWebElement loginButton = wait.Until(driver => driver.FindElement(By.Id("login")));
				loginButton.Click();

				// Wait for the email input field and enter the username
				driver.FindElement(By.Id("Input_Email")).SendKeys("rahulk4784782@gmail.com");

				// Wait for the password input field and enter the password
				driver.FindElement(By.Id("Input_Password")).SendKeys("Walnut@1409");

				// Wait for the login-submit button and click it
				//IWebElement loginSubmitButton = wait.Until(driver => driver.FindElement(By.Id("login-submit")));
				//loginSubmitButton.Click();

				driver.FindElement(By.Id("account")).Submit(); // Replace "login-form" with the form's ID.

				

				IWebElement productDetailsButton = wait.Until(driver => driver.FindElement(By.CssSelector("a.btn.btn-primary.bg-gradient.border-0.form-control[href*='Details']")));

				// Try multiple approaches to click the button
				try
				{
					// First attempt: Scroll into view and click
					((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", productDetailsButton);
					Thread.Sleep(500); // Give the page a moment to settle after scrolling
					productDetailsButton.Click();
				}
				catch (ElementClickInterceptedException)
				{
					try
					{
						// Second attempt: Use JavaScript click
						((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", productDetailsButton);
					}
					catch (Exception)
					{
						// Third attempt: Actions class
						Actions actions = new Actions(driver);
						actions.MoveToElement(productDetailsButton).Click().Perform();
					}
				}

				//IWebElement addToCartButton = wait.Until(driver => driver.FindElement(By.CssSelector("button.btn.btn-primary.bg-gradient.w-100.py-2.text-uppercase.fw-semibold[type='submit']")));
				//addToCartButton.Click();

				// Wait for the product details page to load and find Add to Cart button
				IWebElement addToCartButton = wait.Until(driver =>
					driver.FindElement(By.CssSelector("button.btn.btn-primary.bg-gradient.w-100.py-2.text-uppercase.fw-semibold[type='submit']")));

				// Wait for the button to be clickable
				wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return window.getComputedStyle(arguments[0]).getPropertyValue('visibility')", addToCartButton).Equals("visible"));

				// Try multiple approaches to click the Add to Cart button
				try
				{
					// First attempt: Scroll to bottom of page and then to element
					((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
					Thread.Sleep(500);
					((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({ behavior: 'smooth', block: 'center' });", addToCartButton);
					Thread.Sleep(1000); // Give more time for the page to settle

					wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(addToCartButton));
					addToCartButton.Click();
				}
				catch (ElementClickInterceptedException)
				{
					try
					{
						// Second attempt: Use JavaScript click with a custom scroll
						((IJavaScriptExecutor)driver).ExecuteScript(@"
                    arguments[0].scrollIntoView(true);
                    window.scrollBy(0, -100);  // Scroll up a bit to avoid any headers
                    arguments[0].click();
                ", addToCartButton);
					}
					catch (Exception)
					{
						// Third attempt: Actions class with pause
						Actions actions = new Actions(driver);
						actions
							.MoveToElement(addToCartButton)
							.Pause(TimeSpan.FromMilliseconds(500))
							.Click()
							.Perform();
					}
				}

			}
			catch (NoSuchElementException ex)
			{
				Console.WriteLine($"Element not found: {ex.Message}");
			}
			catch (WebDriverException ex)
			{
				Console.WriteLine($"WebDriver error: {ex.Message}");
			}
			//finally
			//{
			//	// Close the WebDriver
			//	//driver.Quit();
			//}
		}



		//[Test]
		//public void BulkyTest()
		//{
		//	IWebDriver driver = new EdgeDriver();

		//	driver.Navigate().GoToUrl(""); //navigate to url

		//	//driver.Manage().Window.Maximize(); //maximizing the browser window

		//	IWebElement webelement = driver.FindElement(By.Id("login")); //find the element

		//	webelement.Click(); //click on login

		//	IWebElement findusername = driver.FindElement(By.Id("Input_Email"));

		//	findusername.SendKeys("rahulk4784782@gmail.com"); //typing the username

		//	IWebElement findpassword = driver.FindElement(By.Id("Input_Password"));

		//	findpassword.SendKeys("Walnut@1409"); //typing the password

		//	IWebElement clicklogin = driver.FindElement(By.Id("login-submit"));

		//	if (clicklogin.Displayed && clicklogin.Enabled)
		//	{
		//		clicklogin.Click();
		//	}
		//	else
		//	{
		//		Console.WriteLine("The button is either not displayed or not enabled.");
		//	}
		//	//clicklogin.Click();

		//}
	}
}