import { Component } from '@angular/core';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styles: [`
        .div-wrapper {
          margin-right: 300px;
        }
        .div-lounge {
          float: left;
          width: 100%;

        }
        .div-lounge div{
           margin:0 0 10px 0;
           border: 1px solid #9BCCE0;
           background-color: #DDF0D5;
        }
        .div-explore {
          float: right;
          width: 300px;
          margin-right: -300px;
        }
       .div-explore .top{
           margin:0 10px 10px 10px;
           border: 1px solid #9BCCE0;
           background-color: #DDF0D5;
        }
        .div-explore .bot{
           margin:10px 10px 10px 10px;
           border: 1px solid #9BCCE0;
           background-color: #DDF0D5;
        }
        .div-clear {
          clear: both;
        }

    `]
})
export class HomeComponent {
    public title = "Aplikacja wspomagająca zarządzanie projektami";
    public info = "Jest to aplikacja pozwalająca na zarządzanie projektami studentów."
    public autor = "Szymon Krawieczek"
}
