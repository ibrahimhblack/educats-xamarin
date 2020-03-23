﻿using EduCATS.Data.Models.User;
using EduCATS.Helpers.Settings;

namespace EduCATS.Data.User
{
	/// <summary>
	/// App user data manager.
	/// </summary>
	public class AppUserData
	{
		/// <summary>
		/// Professor user type.
		/// </summary>
		const string _professorType = "1";

		/// <summary>
		/// Student user type.
		/// </summary>
		const string _studentType = "2";

		/// <summary>
		/// User ID.
		/// </summary>
		public static int UserId { get; set; }

		/// <summary>
		/// User full name.
		/// </summary>
		public static string Name { get; set; }

		/// <summary>
		/// User group ID.
		/// </summary>
		public static int GroupId { get; set; }

		/// <summary>
		/// User profile image.
		/// </summary>
		/// <remarks>
		/// Represented in base64 string.
		/// </remarks>
		public static string Avatar { get; set; }

		/// <summary>
		/// Username.
		/// </summary>
		public static string Username { get; set; }

		/// <summary>
		/// User group.
		/// </summary>
		public static string GroupName { get; set; }

		/// <summary>
		/// User type.
		/// </summary>
		public static UserTypeEnum UserType { get; set; }

		/// <summary>
		/// Set login data.
		/// </summary>
		/// <param name="userId">User ID.</param>
		/// <param name="username">Username.</param>
		public static void SetLoginData(int userId, string username)
		{
			AppPrefs.UserId = userId;
			AppPrefs.UserLogin = username;

			UserId = userId;
			Username = username;
		}

		/// <summary>
		/// Set profile data.
		/// </summary>
		/// <param name="profile">Profile model.</param>
		public static void SetProfileData(UserProfileModel profile)
		{
			if (profile == null) {
				return;
			}

			GroupId = profile.GroupId;
			GroupName = profile.GroupName;
			Avatar = profile.Avatar;
			Name = profile.Name;

			AppPrefs.Avatar = Avatar;
			AppPrefs.GroupName = GroupName;

			switch (profile.UserType) {
				case _professorType:
					UserType = UserTypeEnum.Professor;
					break;
				case _studentType:
					UserType = UserTypeEnum.Student;
					break;
			}
		}

		/// <summary>
		/// Reset app user manager.
		/// </summary>
		public static void Clear()
		{
			UserId = 0;
			GroupId = 0;
			UserType = 0;
			Name = null;
			Avatar = null;
			Username = null;
			GroupName = null;
		}
	}
}
