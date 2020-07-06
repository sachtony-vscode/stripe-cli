// Set your secret key. Remember to switch to your live secret key in production!
    // See your keys here: https://dashboard.stripe.com/account/apikeys
    CardholderCreateParams params =
        CardholderCreateParams.builder()
            .setName("Jenny Rosen")
            .setEmail("jenny.rosen@shops.sachtony.com")
            .setPhoneNumber("+18008675309")
            .setStatus(CardholderCreateParams.Status.ACTIVE)
            .setType(CardholderCreateParams.Type.INDIVIDUAL)
            .setBilling(
            CardholderCreateParams.Billing.builder()
                .setAddress(
                CardholderCreateParams.Billing.Address.builder()
                    .setLine1("1234 Main Street")
                    .setCity("San Francisco")
                    .setState("CA")
                    .setPostalCode("94111")
                    .setCountry("US")
                    .build())
                .build())
            .build();

        Cardholder cardholder = Cardholder.create(params);

    CardCreateParams params =
        CardCreateParams.builder()
            .setCardholder("ich_1Cm3pZIyNTgGDVfzI83rasFP")
            .setType(CardCreateParams.Type.VIRTUAL)
            .setCurrency("usd")
            .build();

        Card card = Card.create(params);


    // Set your secret key. Remember to switch to your live secret key in production!
    // See your keys here: https://dashboard.stripe.com/account/apikeys

    Card card = Card.retrieve("ic_1CoYuRKEl2ztzE5GIEDjQiUI");

    CardUpdateParams params =
        CardUpdateParams.builder()
            .setStatus(CardUpdateParams.Status.ACTIVE)
            .build();

        card.update(params),
