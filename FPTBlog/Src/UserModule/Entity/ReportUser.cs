namespace FPTBlog.Src.UserModule.Entity {
    public class ReportUser {

        public ReportUser() {
            this.StudentThisMonth = 0;
            this.StudentLastMonth = 0;
            this.LecturerThisMonth = 0;
            this.LecturerLastMonth = 0;
        }
        public int StudentThisMonth {
            get; set;
        }
        public int StudentLastMonth {
            get; set;
        }
        public int LecturerThisMonth {
            get; set;
        }
        public int LecturerLastMonth {
            get; set;
        }
    }
}