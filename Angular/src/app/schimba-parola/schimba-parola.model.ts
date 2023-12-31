export interface SchimbaParola {
    userName: string;
    currentPassword: string;
    newPassword: string;
    confirmPassword: string;
}

export interface SchimbaParolaResponse {
    statut: string;
    mesaj: string;
}