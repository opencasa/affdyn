import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class EvenementsStarteo extends React.Component<RouteComponentProps<{}>> {
    constructor(props: any) {
        super(props)
    }
    // todo : bouton accueil attention chemin
    public render() {
        return (
            <div>
                <a className='btn' href='/starteo' >
                    <span className='glyphicon glyphicon-home'></span> Accueil
                </a>

                <video id="avid" loop autoPlay>
                    <source src="./dist/assets/starteo/videos/evenements.mp4" type="video/mp4" />
                </video>
            </div>
        );
    }
}
