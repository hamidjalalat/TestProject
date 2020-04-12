using System.Linq;
using System.Data.Entity;

namespace Models
{
	internal class DatabaseContextInitializer :
		System.Data.Entity.DropCreateDatabaseIfModelChanges<DataBaseContext>
	{
		public DatabaseContextInitializer() : base()
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
		protected override void Seed(DataBaseContext databaseContext)
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

			

			// Optional
			databaseContext.SaveChanges();
		}
	}
}
