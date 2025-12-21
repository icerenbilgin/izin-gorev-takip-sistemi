import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class UserContextService {

    private readonly STORAGE_KEY = 'user';

    get user(): any | null {
        const data = localStorage.getItem(this.STORAGE_KEY);
        return data ? JSON.parse(data) : null;
    }

    get userId(): number | null {
        return this.user?.userId ?? null;
    }

    get userRoleId(): number | null {
        return this.user?.userRoleId ?? null;
    }

    get isLoggedIn(): boolean {
        return !!this.userId;
    }

    clear(): void {
        localStorage.removeItem(this.STORAGE_KEY);
    }
}
