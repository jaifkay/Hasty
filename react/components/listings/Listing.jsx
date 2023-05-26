import React, { useState } from 'react';
import { Carousel, Col } from 'react-bootstrap';
import PropTypes from 'prop-types';
import './listings.css';
import housePlaceholder from '../../assets/images/listings/housePlaceholder.png';
import Ratings from '../rating/Ratings';
import * as ratingsService from '../../services/ratingsService';
import { useNavigate } from 'react-router';

function Listing(props) {
  const listing = props.listing;
  const { lgCol, mdCol, smCol } = props;
  const navigate = useNavigate();
  const [index, setIndex] = useState(0);
  const handleSelect = (selectedIndex) => {
    setIndex(selectedIndex);
  };

  const navigateToDetails = (e) => {
    e.preventDefault();
    const stateForTransport = {
      type: 'LISTING_VIEW',
      payload: listing,
    };
    navigate(`/listings/${listing.id}`, { state: stateForTransport });
  };

  const mappedImages = listing.images.map((image) => {
    return (
      <Carousel.Item key={image.fileId}>
        <img className="listing-img" src={image.url} alt={image.name} onClick={navigateToDetails} />
      </Carousel.Item>
    );
  });

  return (
    <Col lg={lgCol} md={mdCol} sm={smCol} className="mb-2 mt-2">
      <div className="d-block card listing-card">
        {listing.images ? (
          <Carousel indicators={false} activeIndex={index} interval={null} onSelect={handleSelect}>
            {mappedImages}
          </Carousel>
        ) : (
          <img className="listing-img" src={housePlaceholder} alt={housePlaceholder} />
        )}
        <div className="card-body" onClick={navigateToDetails}>
          <h5 className="card-title d-flex justify-content-center">{listing.location.lineOne}</h5>
          <p className="card-text d-flex justify-content-center">
            <strong>
              {listing.location.city} {listing.location.state.name}, {listing.location.zip}
            </strong>
          </p>
          <p className="d-flex justify-content-around">
            {listing.bedRooms} Bed - {listing.baths} Bath - {listing.accessType.name}
          </p>
          <p className="d-flex justify-content-around">
            <strong>${listing.costPerNight} per Night</strong>
          </p>
          <div className="text-center">
            <Ratings
              isReadOnly={true}
              entityId={listing.id}
              entityTypeId={ratingsService.entityTypes.LISTING}></Ratings>
          </div>
        </div>
      </div>
    </Col>
  );
}

Listing.propTypes = {
  listing: PropTypes.shape({
    id: PropTypes.number.isRequired,
    internalName: PropTypes.string.isRequired,
    title: PropTypes.string.isRequired,
    shortDescription: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired,
    bedRooms: PropTypes.number.isRequired,
    baths: PropTypes.number.isRequired,
    housingType: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    }),
    accessType: PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
    }),
    listingServices: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
      })
    ),
    listingAmenities: PropTypes.arrayOf(
      PropTypes.shape({
        id: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
      })
    ),
    images: PropTypes.arrayOf(
      PropTypes.shape({
        fileId: PropTypes.number.isRequired,
        fileTypeId: PropTypes.number.isRequired,
        name: PropTypes.string.isRequired,
        url: PropTypes.string.isRequired,
      })
    ),
    guestCapacity: PropTypes.number.isRequired,
    costPerNight: PropTypes.number.isRequired,
    costPerWeek: PropTypes.number.isRequired,
    checkInTime: PropTypes.string.isRequired,
    checkOutTime: PropTypes.string.isRequired,
    daysAvailable: PropTypes.number.isRequired,
    location: PropTypes.shape({
      id: PropTypes.number.isRequired,
      locationType: PropTypes.shape({
        id: PropTypes.number.isRequired,
      }),
      lineOne: PropTypes.string.isRequired,
      city: PropTypes.string.isRequired,
      state: PropTypes.shape({
        name: PropTypes.string.isRequired,
      }),
      zip: PropTypes.string.isRequired,
      latitude: PropTypes.number.isRequired,
      longitude: PropTypes.number.isRequired,
    }),
    hasVerifiedOwnerShip: PropTypes.bool.isRequired,
    isActive: PropTypes.bool.isRequired,
    createdBy: PropTypes.number.isRequired,
    dateCreated: PropTypes.string.isRequired,
    dateModified: PropTypes.string.isRequired,
  }),
  lgCol: PropTypes.number.isRequired,
  mdCol: PropTypes.number.isRequired,
  smCol: PropTypes.number.isRequired,
  isReadOnly: PropTypes.bool.isRequired,
};

export default Listing;
