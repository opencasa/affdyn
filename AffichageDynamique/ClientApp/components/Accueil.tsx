import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Accueil extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return (
            <div>
                <div className="menu-container">
                    <video id="bgvid" loop autoPlay>
                        <source src="./dist/assets/bp/videos/accueil.mp4" type="video/mp4" />
                    </video>
                    <a href="/rechercheentreprises"><img className="tlbox" src="./dist/assets/bp/accueil/entreprises.png" alt="Entreprises" /></a>
                    <a href="/partenaires"><img className="trbox" src="./dist/assets/bp/accueil/partenaires.png" alt="Partenaires" /></a>
                    <a href="/evenements"><img className="blbox" src="./dist/assets/bp/accueil/evenements.png" alt="Ev&eacute;nements" /></a>
                    <a href="/presentation"><img className="brbox" src="./dist/assets/bp/accueil/presentation.png" alt="Presentation" /></a>
                    <a href="/apropos"><img className="centerbox" src="./dist/assets/bp/accueil/cercle.png" alt="Cercle" /></a>
                    <img className="logoaccueil" src="./dist/assets/bp/accueil/logo.png" alt="Entreprises" />
                </div>
            </div>
            );
    }
}
