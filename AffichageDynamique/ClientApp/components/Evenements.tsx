import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Evenements extends React.Component<RouteComponentProps<{}>> {
    constructor(props: any) {
        super(props)
    }

    public render() {
        return (   
            <div>
                <a className='btn' href='/' >
                    <span className='glyphicon glyphicon-home'></span> Accueil
                </a>

                <video id="avid" loop autoPlay>
                    <source src="./dist/assets/videos/evenements.mp4" type="video/mp4" />
                </video>   
            </div>
        );
    }
}
