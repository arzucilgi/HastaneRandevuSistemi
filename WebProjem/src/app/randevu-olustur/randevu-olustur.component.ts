import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-randevu-olustur',
  imports: [FormsModule],
  templateUrl: './randevu-olustur.component.html',
  styleUrl: './randevu-olustur.component.css'
})
export class RandevuOlusturComponent {
  constructor(
    private http: HttpClient,
    private router: Router
 
  ) {}
 
  sehirler: any[] = [];
  hastaneler: any[] = [];
  poliklinikler: any[] = [];
  doktorlar: any[] = [];

  girisYapanKullaniciId : number=Number(localStorage.getItem('userId'))

  secilenSehir: string = '';
  secilenSehirId: number | null = null;  // Şehir ID'si buraya atanacak
  secilenHastane: string = '';
  secilenHastaneId:number| null =null
  secilenPoliklinik: string = '';
  secilenPoliklinikId:number | null=null;
  secilenDoktor: string = '';

  ngOnInit() {
    this.getSehirler();
  }

  getSehirler() {

    this.http.get<any[]>('http://localhost:5153/api/Sehirler').subscribe({
      next: (data) =>{
        console.log(data),
        (this.sehirler=data)
        console.log(this.secilenSehir)
      
      },
      error: (err) =>{
        console.error('Şehirler yüklenirken hata oluştu:', err),
        alert('Şehirler yüklenirken hata oluştu')
      }
    });
  }

  getHastaneler() {
    const selectedCity = this.sehirler.find(sehir => sehir.sehirName === this.secilenSehir);

    if (selectedCity) {
      this.secilenSehirId = selectedCity.id;  // Şehir id'sini aldım.
      console.log('Seçilen Şehir ID:', this.secilenSehirId);
      //withCredentials: true  cookie'yi otomatik olarak gönderir
      this.http.get<any[]>(`http://localhost:5153/api/Hastane/${this.secilenSehirId}`, { withCredentials: true }).subscribe({
        next: (data) => {
          console.log('Hastaneler:', data);
          this.hastaneler = data;
           
        },
        error: (err) =>
          {
            console.error('Hastaneler yüklenirken hata oluştu:', err),
            alert('Seçilen şehirde hastane bulunamadı!')
          }
      });
    } 
  }

  getPoliklinikler() {
    const selectedHospital = this.hastaneler.find(hastane => hastane.hastaneName === this.secilenHastane);
    if(selectedHospital){
    this.secilenHastaneId=selectedHospital.id
    console.log('Seçilen Hastane ID:', this.secilenHastaneId);
    this.http.get<any[]>(`http://localhost:5153/api/Poliklinik/Hastane/${this.secilenHastaneId}`, { withCredentials: true }).subscribe({
        next: (data) => (this.poliklinikler = data),
        error: (err) => {
          console.error('Poliklinikler yüklenirken hata oluştu:', err),
          alert('Seçilen  hastanede poliklinik bulunamadı!')
        }
      });
    }
    
  }

  getDoktorlar() {
    const selectedPolicklinic = this.poliklinikler.find(poliklinik => poliklinik.poliklinikName === this.secilenPoliklinik);
    if(selectedPolicklinic){
      this.secilenPoliklinikId=selectedPolicklinic.id
      console.log('Seçilen Poliklinik ID:', this.secilenPoliklinikId);
      this.http.get<any[]>(`http://localhost:5153/api/Doktor/${this.secilenPoliklinikId}`, { withCredentials: true }).subscribe({
        next: (data) => (this.doktorlar = data),
        error: (err) => {
          console.error('Doktorlar yüklenirken hata oluştu:', err),
          alert('Seçilen  poliklinikte doktor bulunamadı!')
        }
      });
    }
   
  }


  randevuOlustur() {
    const randevuBilgileri = {
      kullaniciId:this.girisYapanKullaniciId,
      sehirId: this.secilenSehirId,
      hastaneId: this.secilenHastaneId,
      poliklinikId: this.secilenPoliklinikId,
      doktorId: this.doktorlar.find(doktor => doktor.doktorName + ' ' + doktor.doktorSurname === this.secilenDoktor)?.id,
    };

    if(!randevuBilgileri.kullaniciId  ||!randevuBilgileri.sehirId || !randevuBilgileri.hastaneId || !randevuBilgileri.poliklinikId || !randevuBilgileri.doktorId) {
      alert('Lütfen tüm seçimleri yapınız!');
      return;
    }
  
    this.http.post('http://localhost:5153/api/Randevu', randevuBilgileri, { withCredentials: true }).subscribe({
      next: (response) => {
        alert('Randevu başarıyla oluşturuldu!');
        console.log('Oluşturulan randevu:', response);
      },
      error: (err) => {
        console.error('Randevu oluşturulurken hata oluştu:', err);
        alert('Randevu oluşturulurken bir hata oluştu.');
      }

      
    });
    localStorage.setItem('secilenSehir', JSON.stringify(this.secilenSehir));
    localStorage.setItem('secilenHastane', JSON.stringify(this.secilenHastane));
    localStorage.setItem('secilenPoliklinik', JSON.stringify(this.secilenPoliklinik));
    localStorage.setItem('secilenDoktor', JSON.stringify(this.secilenDoktor));

    this.router.navigate(['/home/randevuBilgileri']);
  }
  

}
