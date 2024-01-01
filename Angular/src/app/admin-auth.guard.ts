import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AutentificareService } from "./autentificare.service";

@Injectable({
    providedIn: 'root'
})
export class AdminAuthGuard {
    constructor(private router: Router, private autentificateService: AutentificareService) {}

    canActivate(): boolean {
        return this.verifAdmin();
    }

    private verifAdmin(): boolean {
        if(this.autentificateService.verifAutentificare()){
            var roluri = this.autentificateService.getRoluriFromToken();

            if(roluri?.includes('Admin'))
                return true;
            return false;
        }
        return false;
    }
}