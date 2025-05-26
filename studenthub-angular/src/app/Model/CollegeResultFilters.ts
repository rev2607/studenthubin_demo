
export class CollegesResultsFilters {
  
        public StatesAndCities: StateCities[]; 
        public CollegeTypes:string[];
        public CollegeSubTypes:string[];

}

export class StateCities {
    public State: string;
    public isChecked:boolean;
    public Cities: string[];
    public isHidden:boolean;
}

export class InputBind {
    public Name: string;
    public isChecked: boolean;
    public isVisible: boolean;
}


export class CollegeResults
{
    public collegesCount: number;
    public colleges: Array<College>;
}

export class College {
    public  Name: string;
    public  ID: number;
    public  State: string;
    public  City: string;
    public  Pincode: string
    public  CoverPic: string;
    public  Logo: string;
    public  TypeOfInstitute: string
    public  SubType: string;
    public  Stream: string;
    public Knownas: string;
    public AcademicDepartments: string;
    public AcademicCentres: string;
    public FacultyNo: string;
    public StudentsIntake: string;
    public StudentsDiversity: string;
    public FacultytoStudentRatio: string;
    public AnnualExpenditure: string;
    public ResearchDetails: string;
    public Location: string;
    public Approval: string;
    public EstablishedYear: string;
    public CampusSize: string;
    public Degree_4Years: string;
    public Degree_5Years: string;
    public Degree_3Years: string;
    public ModeOfAdmission: string;
    public FeesStructure: string;
    public HostelFee: string;
    public FeeWaivers: string;
    public Scholarships: string;  
    public CampusFacilities: string;
    public  Rankings: string;     
    public Connectivity: string; 
    public  Website: string;
    public Phone1: string;
    public Email1: string;
    public AveragePackage: string;
    public HighestPackage: string;
    public LowestPackage: string;
    public Recruiters: string;
    public Type: string;
    public Address: string;
}
