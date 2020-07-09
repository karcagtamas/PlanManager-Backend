using System;
using ManagerAPI.Models.Entities;
using ManagerAPI.Models.Models;
using ManagerAPI.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ManagerAPI.Services.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly IUtilsService _utilsService;

        public LoggerService(ILogger<LoggerService> logger, IUtilsService utilsService) {
            this._logger = logger;
            this._utilsService = utilsService;
        }
        
        /// <summary>
        /// Join user to the given message
        /// </summary>
        /// <param name="message">Source message</param>
        /// <param name="user">User for join</param>
        /// <returns>Joined message</returns>
        public string AddUserToMessage (string message, User user) {
            return $"Invalid action for user {user.UserName} ({user.Id}): {message}";
        }

        /// <summary>
        /// Log error to the console
        /// </summary>
        /// <param name="e">Exception for logging</param>
        /// <returns>Error Response from Exception</returns>
        public void LogError (Exception e) {
            _logger.LogError (e.Message);
        }

        /// <summary>
        /// Log executed events
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="service">Service's name</param>
        /// <param name="action">Executed action</param>
        /// <param name="entity">Connected entity object</param>
        public void LogInformation (User user, string service, string action, int id, object entity) {
            this.LogInformation(user, service, action, id.ToString(), entity);
        }

        /// <summary>
        /// Log executed events
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="service">Service's name</param>
        /// <param name="action">Executed action</param>
        public void LogInformation(User user, string service, string action, int id)
        {
            this.LogInformation(user, service, action, id);
        }

        /// <summary>
        /// Exception to Error response.
        /// To API sending.
        /// </summary>
        /// <param name="e">Error</param>
        /// <returns>API Error response</returns>
        public ErrorResponse ExceptionToResponse (Exception e) {
            this.LogError (e);
            return new ErrorResponse (e);
        }

        /// <summary>
        /// Log invalid things.
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="service">Service's name</param>
        /// <param name="thing">Thing</param>
        /// <returns>Invalid message</returns>
        public MessageException LogInvalidThings(User user, string service, string thing, string message)
        {
            string end = $"Invalid {thing}";
            _logger.LogError($"{this._utilsService.UserDisplay(user)}: {service} - {end}");
            return new MessageException(message);
        }

        public void LogInformation(User user, string service, string action, string id)
        {
            this.LogInformation(user, service, action, id);
        }

        public void LogInformation(User user, string service, string action, string id, object entity)
        {
            _logger.LogInformation($"{this._utilsService.UserDisplay(user)}: {service} - {action.ToUpper()} - with id: {id}");
            if (entity != null) {
                _logger.LogInformation(entity.ToString());
            }
        }
    }
}