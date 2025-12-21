import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {

    private hubConnection!: signalR.HubConnection;

    startConnection(userId: number): void {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`https://localhost:5121/hubs/notification?userId=${userId}`)
            .withAutomaticReconnect()
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('SignalR bağlantısı kuruldu'))
            .catch(err => console.error('SignalR bağlantı hatası:', err));
    }

    onReceiveNotification(callback: (message: string) => void): void {
        this.hubConnection.on('ReceiveNotification', callback);
    }

    stopConnection(): void {
        if (this.hubConnection) {
            this.hubConnection.stop();
        }
    }
}