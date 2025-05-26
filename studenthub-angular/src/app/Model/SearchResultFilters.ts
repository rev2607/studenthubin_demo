    export class Exam {
        ExamName: string;
      ExamId: string;
      constructor(examName: string, examId: string) {
        this.ExamName = examName;
        this.ExamId = examId;
      }
    }

    export class ExamsDetail {
        ExamName: string;
        ExamId: string;
        ExamType: string;
        State: string;
        Year: string ="";
        Month: string ="";
        ReleaseDate: string="";
        University: string ="";

      constructor(examName: string, examId: string, examType: string, state: string) {
        this.ExamName = examName;
        this.ExamId = examId;
        this.ExamType = examType;
        this.State = state;
      }   
    }

export class SearchResultFilters {
        States: string[];
        ExamTypes: string[];
        Exams: Exam[];
        ExamsDetails: ExamsDetail[];
        Years: string[];
SearchType: string[];
  constructor(states: string[], examTypes: string[], exams: Exam[], examsDetails: ExamsDetail[], years: string[], searchType: string[]) {
    this.States = states;
    this.ExamTypes = examTypes;
    this.Exams = exams;
    this.ExamsDetails = examsDetails;
    this.Years = years;
    this.SearchType = searchType;
  }
    }



