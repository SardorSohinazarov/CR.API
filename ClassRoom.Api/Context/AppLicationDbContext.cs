using ClassRoom.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Api.Context;

public class AppLicationDbContext:IdentityDbContext<User,Role,Guid>
{
	public AppLicationDbContext(DbContextOptions options)
		:base(options)
	{

	}
}
