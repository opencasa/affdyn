import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    
                    {/*<Link className='navbar-brand' to={'/'} >
                        <span className='glyphicon glyphicon-home'></span> Accueil
                            </Link> */}
                </div>
                {/*<div className='clearfix'></div>  nav navbar-nav nav-justified pull-left*/}
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink to={'/'} exact activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Accueil
                            </NavLink>
                        </li>
                       {/* <li>
                            <NavLink to={'/editentreprises'} activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Entreprises
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/partenaires'} activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Partenaires
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/evenements'} activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Ev&eacute;nements
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/recherche'} activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Recherche
                            </NavLink>
                        </li>*/}

                    </ul>
                </div>
                </div>
        </div>;
    }
}