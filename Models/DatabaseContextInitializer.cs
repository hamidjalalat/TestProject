using System.Linq;
using System.Data.Entity;

namespace Models
{
	internal static class DatabaseContextInitializer
	{
		static DatabaseContextInitializer()
		{
		}

		//protected override void Seed(DatabaseContext context)
		//{
		//	base.Seed(context);
		//}

		/// <summary>
		/// اين تابع فقط پس از ايجاد بانک اطلاعاتی فرآخوانی می گردد
		/// در صورتی که بانک اطلاعاتی وجود داشته باشد، اجرا نخواهد شد
		/// </summary>
		internal static void Seed(DataBaseContext databaseContext)
		{
			// دقت داشته باشید که وقتی وارد
			// Seed
			// می‌شویم، معنی آن این است که بانک اطلاعاتی و جداول ایجاد شده‌اند
			// لذا اگر در داخل این تابع به خطایی برخورد نماییم، حواسمان باشد
			// که برای سری بعد، خودمان بانک اطلاعاتی را در ابتدا حذف نماییم

			// اطلاعات پایه

			// اطلاعات تستی

			GroupProduct oGroupProduct = null;

		

			oGroupProduct = new GroupProduct();
			oGroupProduct.Name = "پیتزا";
			databaseContext.GroupProducts.Add(oGroupProduct);

			oGroupProduct = new GroupProduct();
			oGroupProduct.Name = "پرسی";
			databaseContext.GroupProducts.Add(oGroupProduct);

			oGroupProduct = new GroupProduct();
			oGroupProduct.Name = "ساندویج";
			databaseContext.GroupProducts.Add(oGroupProduct);

			oGroupProduct = new GroupProduct();
			oGroupProduct.Name = "نوشیدنی";
			databaseContext.GroupProducts.Add(oGroupProduct);

			Config oConfig = null;
			oConfig = new Config();
			oConfig.Name = "non";
			oConfig.Value = "1000";
			oConfig.Text = "قیمت نان";

			databaseContext.Configs.Add(oConfig);


			// Optional
			databaseContext.SaveChanges();
		}
	}
}
