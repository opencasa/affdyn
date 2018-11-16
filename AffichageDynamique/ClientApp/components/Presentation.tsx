import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Presentation extends React.Component<RouteComponentProps<{}>> {
    constructor(props: any) {
        super(props)
    }

    public render() {
        return (
            <div>
                <a className='btn' href='/' >
                    <span className='glyphicon glyphicon-home'></span> Accueil
                </a>
                <video id="bgvid" loop autoPlay>
                    <source src="./dist/assets/videos/prezbp.mp4" type="video/mp4" />
                </video>
            </div>
        );
    }
}
