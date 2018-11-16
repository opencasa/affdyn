// A '.tsx' file enables JSX support in the TypeScript compiler,
// for more information see the following page on the TypeScript wiki:
// https://github.com/Microsoft/TypeScript/wiki/JSX
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

interface OrganisationState {
    organisationsPaginated: OrganisationsPaginated;
    currOrga: Organisation;
    currPage: number;
}
class Helpers extends React.Component {
    static GetEmptyOrganisation() {
        var o: Organisation = {
            id: 0,
            nomOrganisation: '',
            domaine: '',
            slug: '',
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
            etat: 0
        };
        return o;
    }
}
export class EditEntreprises extends React.Component<any, OrganisationState> {
    constructor(props: any) {
        super(props);
        this.state = {
            currPage: 1,
            currOrga: Helpers.GetEmptyOrganisation(),
            organisationsPaginated: { organisations: [], totalPages: 1 }
        };

        fetch('api/Data/GetOrganisations')
            .then(response => response.json() as Promise<OrganisationsPaginated>)
            .then(data => {
                this.setState({ organisationsPaginated: data });
            });
    }

    public render() {
        let orgas = this.state.organisationsPaginated.organisations;
        return (
            <div>
                <h2>Liste des entreprises</h2>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>id</th>
                            <th>Nom</th>
                            <th>Domaine</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {orgas.map((o) =>
                            <tr key={o.id}>
                                <td>{o.id}</td>
                                <td>{o.nomOrganisation}</td>
                                <td>{o.domaine}</td>
                                <td><a onClick={() => this.FillFormForUpdate(o)}>Modifier</a></td>
                            </tr>
                        )}
                    </tbody>
                </table>

                <AddEditOrganisation
                    organisation={this.state.currOrga}
                    actionAdd={this.AddUpdate.bind(this)}
                    actionReset={this.ResetForm.bind(this)}
                />
            </div>
        );
    }

    AddUpdate(o: Organisation) {
        console.log(o.id);
        var isUpdate: boolean = o.id > 0;
        if (o.nomOrganisation == '') return;
        var orgas: Organisation[] = this.state.organisationsPaginated.organisations;
        fetch('/api/Data/CreateUpdateOrganisation', {
            method: 'post',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }),
            body: JSON.stringify(o)
        })
            .then(response => response.json() as Promise<Organisation[]>)
            .then(data => {
                if (isUpdate) {
                    for (var i = 0; i < orgas.length; i++) {
                        if (orgas[i].id == o.id) {
                            orgas[i] = data[0];
                        }
                    }
                }
                else {
                    orgas.unshift(data[0]);
                    orgas.pop();
                }

                var op: OrganisationsPaginated = { organisations: orgas, totalPages: this.state.organisationsPaginated.totalPages };
                this.setState({ organisationsPaginated: op });

            });

    }

    ResetForm() {
        this.setState({ currOrga: Helpers.GetEmptyOrganisation() });
    }
    FillFormForUpdate(o: Organisation) {
        var o2: Organisation = Helpers.GetEmptyOrganisation();
        o2.id = o.id;
        o2.nomOrganisation = o.nomOrganisation;
        o2.domaine = o.domaine;
        o2.slug = o.slug;
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
        o2.etat = o.etat;

        this.setState({ currOrga: o2 });
        this.render();
    }
}

export class AddEditOrganisation extends React.Component<any, OrganisationState> {
    constructor(props: any) {
        super(props);
    }
    render() {
        var o: Organisation = this.props.organisation;
        return (
            <div className="divAddNew">
                <h2>Ajout/Modification entreprise</h2>
                <div>
                    <a onClick={() => this.ResetForm()}>Reinitialiser</a>
                    <button onClick={this.AddData.bind(this)}>Ajout/Mise &agrave; jour</button>
                </div>
                <div> Id: {o.id}</div>
                <div> nom: <input type="text" value={o.nomOrganisation} onChange={(event) => { o.nomOrganisation = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> domaine: <input type="text" value={o.domaine} onChange={(event) => { o.domaine = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> slug: <input type="text" value={o.slug} onChange={(event) => { o.slug = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> logo: <input type="text" value={o.logo} onChange={(event) => { o.logo = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> image: <input type="text" value={o.image} onChange={(event) => { o.image = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> imageContact: <input type="text" value={o.imageContact} onChange={(event) => { o.imageContact = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> nomContact: <input type="text" value={o.nomContact} onChange={(event) => { o.nomContact = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> telContact: <input type="text" value={o.telContact} onChange={(event) => { o.telContact = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> emailContact: <input type="text" value={o.emailContact} onChange={(event) => { o.emailContact = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> webContact: <input type="text" value={o.webContact} onChange={(event) => { o.webContact = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> effectif: <input type="text" value={o.effectif} onChange={(event) => { o.effectif = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> couleur: <input type="text" value={o.couleur} onChange={(event) => { o.couleur = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>
                <div> etat: <input type="number" value={o.etat} onChange={(event) => { o.etat = parseInt(event.target.value); this.props.organisation = o; this.forceUpdate() }} /></div>
                <div> description: <textarea value={o.description} onChange={(event) => { o.description = event.target.value; this.props.organisation = o; this.forceUpdate(); }} /></div>

            </div>);
    }

    AddData() {
        this.props.actionAdd(this.props.organisation);
    }
    ResetForm() {
        this.props.actionReset(Helpers.GetEmptyOrganisation());
    }

}

