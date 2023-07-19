create schema idt;

create table idt.sec_users (
    user_id                 uuid not null,
    user_name               varchar(50) not null,

    -- pk --
    constraint pk_sec_users primary key (user_id),
    -- ix --
    constraint ix_sec_users unique (user_name)
);

create table idt.authentications (
    user_id                 uuid not null,
    date                    timestamp without time zone,

    -- fk --
    constraint fk_authentications_sec_users foreign key (user_id) references idt.sec_users (user_id)
);