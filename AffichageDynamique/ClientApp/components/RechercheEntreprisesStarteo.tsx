import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface Organisation {
    id: number;
    nomOrganisation: string;
    domaine: string;
    slug: string;
    description: string;
    logo: string;
    image: string;
    imageContact: string;
    nomContact: string;
    telContact: string;
    emailContact: string;
    webContact: string;
    effectif: string;
    couleur: string;
    etat: number;
}

interface OrganisationsPaginated {
    organisations: Organisation[];
    totalPages: number;
}

interface RechercheEntreprisesState {
    organisationsPaginated: OrganisationsPaginated;
    currOrga: Organisation;
    terme: string;
}

class Helpers extends React.Component {
    static GetEmptyOrganisation() {
        var o: Organisation = {
            id: 0,
            nomOrganisation: '',
            domaine: '',
            slug: 'a',
            description: '',
            logo: '',
            image: '',
            imageContact: '',
            nomContact: '',
            telContact: '',
            emailContact: '',
            webContact: '',
            effectif: '',
            couleur: '',
            etat: 1
        };
        return o;
    }
}
export class RechercheEntreprisesStarteo extends React.Component<RouteComponentProps<{}>, RechercheEntreprisesState> {
    constructor(props: any) {
        super(props);
        this.state = {
            currOrga: Helpers.GetEmptyOrganisation(),
            organisationsPaginated: { organisations: [], totalPages: 1 },
            terme: '%'
        };
        
        fetch(`api/Data/RechercheOrganisation?terme=${this.state.terme}&etat=1`) //.then(response => console.log(response.json())
            .then(response => response.json() as Promise<OrganisationsPaginated>)
            .then(data => {
                this.setState({ organisationsPaginated: data });
            });
        this.RechercheLettre('%')
    }

    public render() {
        let orgas = this.state.organisationsPaginated.organisations;
        return (
            <div>
                <video id="bgvid" loop autoPlay>
                    <source src="./dist/assets/starteo/videos/recherche.mp4" type="video/mp4" />
                </video>
                <div className='container-fluid'>
                    <a className='btn' href='/starteo' >
                        <span className='glyphicon glyphicon-home'></span> Accueil
                    </a>
                    <div className='row'>
                        <div className='col-sm-7'>
                            <div>
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/a.png`} onClick={() => this.RechercheLettre('a')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/b.png`} onClick={() => this.RechercheLettre('b')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/c.png`} onClick={() => this.RechercheLettre('c')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/d.png`} onClick={() => this.RechercheLettre('d')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/e.png`} onClick={() => this.RechercheLettre('e')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/f.png`} onClick={() => this.RechercheLettre('f')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/g.png`} onClick={() => this.RechercheLettre('g')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/h.png`} onClick={() => this.RechercheLettre('h')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/i.png`} onClick={() => this.RechercheLettre('i')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/j.png`} onClick={() => this.RechercheLettre('j')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/k.png`} onClick={() => this.RechercheLettre('k')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/l.png`} onClick={() => this.RechercheLettre('l')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/m.png`} onClick={() => this.RechercheLettre('m')} />
                            </div>
                            <div>
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/n.png`} onClick={() => this.RechercheLettre('n')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/o.png`} onClick={() => this.RechercheLettre('o')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/p.png`} onClick={() => this.RechercheLettre('p')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/q.png`} onClick={() => this.RechercheLettre('q')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/r.png`} onClick={() => this.RechercheLettre('r')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/s.png`} onClick={() => this.RechercheLettre('s')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/t.png`} onClick={() => this.RechercheLettre('t')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/u.png`} onClick={() => this.RechercheLettre('u')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/v.png`} onClick={() => this.RechercheLettre('v')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/w.png`} onClick={() => this.RechercheLettre('w')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/x.png`} onClick={() => this.RechercheLettre('x')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/y.png`} onClick={() => this.RechercheLettre('y')} />
                                <img className="imglettre" src={`./dist/assets/starteo/lettres/z.png`} onClick={() => this.RechercheLettre('z')} />

                            </div>
                            <AfficheOrganisation
                                organisation={this.state.currOrga}
                            />
                        </div>
                        <div className='col-sm-5'>
                            <table className='table'>
                                {/*<thead>
                                    <tr>
                                        <th>R&eacute;sultats</th>
                                    </tr>
                                </thead>*/}
                                <tbody>
                                    {orgas.map((o) =>
                                        <tr key={o.id}>
                                            <td><a onClick={() => this.FillFormForUpdate(o)}>{o.nomOrganisation}</a></td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>



            </div>
        );

    }


    RechercheLettre(l: string) {
        this.setState({ terme: l });
        console.log('this.state.terme:' + this.state.terme + ' ' + l);
        // TODO: etat
        fetch(`api/Data/RechercheOrganisation?terme=${l}&etat=1`)
            .then(response => response.json() as Promise<OrganisationsPaginated>)
            .then(data => {
                let orgs: any = data;
                this.setState({ organisationsPaginated: orgs });
                console.log('orgs:' + JSON.stringify(orgs));
                var o2: Organisation = orgs.organisations[0];
                console.log('o2:' + o2);

                if (o2) this.FillFormForUpdate(o2);
            });
    }


    FillFormForUpdate(o: Organisation) {
        console.log('FillFormForUpdate:' + o);
        var o2: Organisation = Helpers.GetEmptyOrganisation();
        o2.id = o.id;
        o2.nomOrganisation = o.nomOrganisation;
        o2.slug = o.slug;
        /*o2.domaine = o.domaine;
        o2.description = o.description;
        o2.logo = o.logo;
        o2.image = o.image;
        o2.imageContact = o.imageContact;
        o2.nomContact = o.nomContact;
        o2.telContact = o.telContact;
        o2.emailContact = o.emailContact;
        o2.webContact = o.webContact;
        o2.effectif = o.effectif;
        o2.couleur = o.couleur;
        o2.etat = o.etat;*/

        this.setState({ currOrga: o2 });
        this.render();
    }
}
export class AfficheOrganisation extends React.Component<any, RechercheEntreprisesState> {
    constructor(props: any) {
        super(props);
    }
    render() {
        var o: Organisation = this.props.organisation;

        return (
            <div className="divAddNew">
                <img className="logoent" src={`./dist/assets/starteo/${o.slug}/${o.slug}.jpg`} alt={`image de ${o.slug}`} />
                {

                    /*<h2>Domaine d'activit&eacute;: {o.domaine}</h2>
                {o.slug ? <img className="logoent" src={`./dist/assets/starteo/${o.slug}/${o.slug}.jpg`} alt={`image de ${o.slug}`} /> : null }
                <div dangerouslySetInnerHTML={{ __html: o.description }} />
                <img className="logoent" src={`./dist/assets/starteo/${o.slug}/image.png`} alt={`image de ${o.slug}`} />
                <div className="bordure">
                    <img className="logoent" src={`./dist/assets/starteo/${o.slug}/logo.png`} alt={`logo de ${o.slug}`} />
                    <img className="logoent" src={`./dist/assets/starteo/${o.slug}/contact.jpg`} alt={`photo de ${o.nomContact}`} />
                    <table>
                        <tr>
                            <td><img className="picto" src={`./dist/assets/starteo/${o.couleur}/nomcontact.png`} alt="nom contact" /></td>
                            <td><p className="textecontact">{o.nomContact}</p></td>
                        </tr>
                        <tr>
                            <td><img className="picto" src={`./dist/assets/starteo/${o.couleur}/tel.png`} alt="t&eacute;l&eacute;phone contact" /></td>
                            <td><p className="textecontact">{o.telContact}</p></td>
                        </tr>
                        <tr>
                            <td><img className="picto" src={`./dist/assets/starteo/${o.couleur}/email.png`} alt="email contact" /></td>
                            <td><p className="textecontact">{o.emailContact}</p></td>
                        </tr>
                        <tr>
                            <td><img className="picto" src={`./dist/assets/starteo/${o.couleur}/web.png`} alt="site web" /></td>
                            <td><p className="textecontact">{o.webContact}</p></td>
                        </tr>
                        <tr>
                            <td><img className="picto" src={`./dist/assets/starteo/${o.couleur}/effectif.png`} alt="effectif" /></td>
                            <td><p className="textecontact">{o.effectif}</p></td>
                        </tr>
                    </table>
                 </div> */}
            </div >
        );
    }

}


