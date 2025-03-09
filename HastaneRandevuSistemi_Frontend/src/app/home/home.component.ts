import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-home',
  imports: [FormsModule,RouterOutlet],
  standalone:true,
  providers: [CookieService],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(
    private http: HttpClient,
    private router: Router,
    private cookieService: CookieService 
 
  ) {}
  cikisYap() {
    // Bu istek, kullanıcının çıkış yapmasını sağlamak için backend'e POST isteği gönderir ve çerezleri iletmek için withCredentials: true parametresini kullanır.
        this.http.post('http://localhost:5153/api/Authentication/Logout',{}, { withCredentials: true}).subscribe({
          next: (response) => {
            console.log('Çıkış başarılı', response);
            
            this.cookieService.delete('token', '/', '.localhost'); 
             
            localStorage.removeItem('userId')
            
    
            this.router.navigate(['/login']);
          },
          error: (error) => {
            console.error('Çıkış hatası:', error);
            alert('Çıkış sırasında bir hata oluştu.');
          }
        });
      }

      randevularimaGit(){
        this.router.navigateByUrl('home/randevularim')
      }
      


}
