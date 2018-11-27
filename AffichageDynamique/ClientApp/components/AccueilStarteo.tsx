import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class AccueilStarteo extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return (
            <div>
                <div className="menu-container">
                    <video id="bgvid" loop autoPlay>
                        <source src="./dist/assets/starteo/videos/accueil.mp4" type="video/mp4" />
                    </video>
                    <a href="/starteo/rechercheentreprises"><img className="tlbox" src="./dist/assets/starteo/accueil/entreprises.png" alt="Entreprises" /></a>
                    <a href="/starteo/partenaires"><img className="trbox" src="./dist/assets/starteo/accueil/partenaires.png" alt="Partenaires" /></a>
                    <a href="/starteo/evenements"><img className="blbox" src="./dist/assets/starteo/accueil/evenements.png" alt="Ev&eacute;nements" /></a>
                    <a href="/starteo/presentation"><img className="brbox" src="./dist/assets/starteo/accueil/presentation.png" alt="Presentation" /></a>
                    <a href="/starteo/apropos"><img className="centerbox" src="./dist/assets/starteo/accueil/cercle.png" alt="Cercle" /></a>
                    <img className="logoaccueil" src="./dist/assets/starteo/accueil/logo.png" alt="Entreprises" />
                </div>
            </div>
        );
    }
}
