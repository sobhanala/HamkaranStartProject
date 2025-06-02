using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class UiSafeExecutor
    {
        private static ILogger Logger => AppLogger.CreateLogger(nameof(UiSafeExecutor));



        public static void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (DatabaseException dbEx)
            {
                Logger.LogWarning(dbEx, "Database warning in UI operation");
                MessageBox.Show(dbEx.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception in UI operation");
                MessageBox.Show("An unexpected error occurred. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static T Execute<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (DatabaseException dbEx)
            {
                Logger.LogWarning(dbEx, "Database warning while executing operation of type {Type}", typeof(T));
                MessageBox.Show(dbEx.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ValidationException vaEx)
            {
                Logger.LogWarning(vaEx, "Validation failed while executing operation of type {Type}", typeof(T));
                MessageBox.Show(vaEx.UserFriendlyMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InventoryException ex)
            {
                Logger.LogError(ex, "Inventory error while executing operation of type {Type}", typeof(T));
                MessageBox.Show("Error: " + ex.UserFriendlyMessage, "Inventory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AuthenticationException auEx)
            {
                Logger.LogError(auEx, "Authentication error while executing operation of type {Type}", typeof(T));
                MessageBox.Show("Error: " + auEx.UserFriendlyMessage, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception while executing operation of type {Type}", typeof(T));
                MessageBox.Show("An unexpected error occurred. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return default;
        }

        public static async Task ExecuteAsync(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (DatabaseException dbEx)
            {
                Logger.LogWarning(dbEx, "Database warning during async UI operation");
                MessageBox.Show(dbEx.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ValidationException ex)
            {
                Logger.LogWarning(ex, "Validation error during async UI operation");
                MessageBox.Show(ex.UserFriendlyMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InventoryException ex)
            {
                Logger.LogError(ex, "Inventory error during async UI operation");
                MessageBox.Show("Error: " + ex.UserFriendlyMessage, "Inventory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception during async UI operation");
                MessageBox.Show("An unexpected error occurred. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (DatabaseException dbEx)
            {
                Logger.LogWarning(dbEx, "Database warning during async execution of type {Type}", typeof(T));
                MessageBox.Show(dbEx.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ValidationException vaEx)
            {
                Logger.LogWarning(vaEx, "Validation failed during async execution of type {Type}", typeof(T));
                MessageBox.Show(vaEx.UserFriendlyMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InventoryException ex)
            {
                Logger.LogError(ex, "Inventory error during async execution of type {Type}", typeof(T));
                MessageBox.Show("Error: " + ex.UserFriendlyMessage, "Inventory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AuthenticationException auEx)
            {
                Logger.LogError(auEx, "Authentication error during async execution of type {Type}", typeof(T));
                MessageBox.Show("Error: " + auEx.UserFriendlyMessage, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Unhandled exception during async execution of type {Type}", typeof(T));
                MessageBox.Show("An unexpected error occurred. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return default;
        }
    }
}
