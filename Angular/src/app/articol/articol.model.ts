export interface Articol {
    id: number;
    titlu: string;
    continut: string;
    utilizatorId: string;
  }
  
  export interface ArticolUtilizatorDto {
    userName: string;
    articole: Articol[]; 
  }