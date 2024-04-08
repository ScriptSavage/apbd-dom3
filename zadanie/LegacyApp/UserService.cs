using System;
using System.Linq;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (isNameAndSurnameEmpty(firstName, lastName))
                return false;


            if (!isEmailValid(email))
                return false;

            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (isAgeValid(age))
            {
                return false;
            }

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool isEmailValid(string email)
        {
            if (email.Contains("@") && email.Contains("."))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private bool isNameAndSurnameEmpty(string name, string surname)
        {
            return (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname));
        }


        private bool isAgeValid(int age)
        {
            return age < 21;
        }


        private void CheckUserCreditLimit(User user, Client client)
        {
            if (client.Type == "VeryImportantClient") user.HasCreditLimit = false;
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    if (client.Type == "ImportantClient") creditLimit *= 2;
                    user.CreditLimit = creditLimit;
                }
            }
        }
    }
}


    

