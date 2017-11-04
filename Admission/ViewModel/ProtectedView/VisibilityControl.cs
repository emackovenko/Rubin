using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Admission.ViewModel;

namespace Admission.ViewModel.ProtectedView
{
	public static class VisibilityControl
	{ 
		/// <summary>
		/// Если для текущего пользователя имеется разрешение на отображение, возвращает true, иначе false
		/// </summary>
		/// <param name="workspaceName">Имя рабочей области</param>
		/// <returns></returns>
		public static bool GetWorkspacePermission(string workspaceName)
		{
			try
			{
				var query = (from permission in Session.DataModel.WorkspacePermissions
						 where permission.RoleId == Session.CurrentUser.RoleId
						 && permission.Workspace.Name == workspaceName
						 select permission).Single();
				return true;
			}
			catch (Exception)
			{
				return false;
			}								
		}
	}
}
